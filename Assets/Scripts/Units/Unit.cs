using UnityEngine;
using System.Collections;
using System;

public class Unit : MonoBehaviour {

    // how long it takes to explode
    const float explodeTime = 0.675f;

    // Use when checking collisions, for example, if it's "Player" or "Enemy"
    public string alliance;

    /// <summary>
    /// [inclusive]
    /// </summary>
    /// <param name="v">percentage, 0-1 float value</param>
    /// <returns></returns>
    internal bool HealthBellowPercetage(float v) {
        Debug.Log(health / maxHealth);
        return ((float)health) / ((float)maxHealth) <= v;
    }

    // Use to distinguish inside same alliance, like different types of powerups.
    public string specialTag;

    float maxHealth;
    public int health;

    public Collider2D unitCollision;
    public MovementInput input;

    Vector3 damage;

    /// <summary>
    /// In which direction will unit move and rotate when exploding
    /// </summary>
    Vector3 explodePhysicsDirection;

    // on death trigger for other scripts
    public bool isDead { get; private set; }

    /// <summary>
    /// Health ui for this unit, optional.
    /// </summary>
    public HealthUi healthUi;
    /// <summary>
    /// Score drop for this unit, optional
    /// </summary>
    public ScoreDrop scoreUi;

    public Powerups powers;

    public bool debugInitWarnings = true;

    // Missing value checks
    void Awake () {
        maxHealth = (float)health;

        if (!debugInitWarnings)
            return;
        if (alliance == "") Debug.LogWarning("Set alliance.", this);
        if (health == 0) Debug.LogWarning("Set hp", this);
        if (unitCollision == null) {
            Debug.LogWarning("Set collider manualy", this);
            unitCollision = GetComponent<Collider2D>();
        }
        if (input == null) {
            Debug.LogWarning("Set ai manualy", this);
            input = GetComponent<AiInput>();
        }

    }



    /// <summary>
    /// Reduce hp and if it's 0 or less, explode.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="damageDirection">Where damage came from, in which direction it explodes.</param>
    internal void ApplyDamage(int damage, Vector3 damageDirection) {
        if (health <= 0) return;

        health -= damage;
        if (healthUi)
            healthUi.SetUi(health);
        if (health <= 0) {
            if (scoreUi)
                scoreUi.Drop();
            isDead = true;
            Explode(damageDirection);
        }
    }

    internal void Explode(Vector3 direction) {
        if (unitCollision)
            unitCollision.enabled = false;
        explodePhysicsDirection = direction;
        StartCoroutine(Explode());
    }

    /// <summary>
    /// Simulate explosion movement for limited time, before destroying it.
    /// </summary>
    /// <returns></returns>
    IEnumerator Explode() {
        float t = Time.time+explodeTime;
        // Disable movement, because explosion physics override movement.
        if (unitCollision)
            unitCollision.enabled = false;
        input.enabled = false;
        
        while (Time.time < t) {
            // TODO: fire explode particles
            input.movement.MoveTowards(explodePhysicsDirection*Time.deltaTime);
            //input.movement.Rotate(explodePhysicsDirection*20);
            yield return t;
        }
        Destroy(gameObject);
    }

    public bool ReceiveCollision(GameObject other) {

        Unit otherUnit= other.GetComponent<Unit>();
        // ignore walls and units on the same side
        if (otherUnit == null || otherUnit.alliance == alliance)
            return false;
        // checks on enemy or powerups
        if (otherUnit.alliance == "player") {
            if (alliance == "enemy") {
                EnemyRamPlayer(otherUnit);
                return true;
            }
        }
        return false;
    }

    

    protected virtual void EnemyRamPlayer(Unit player) {
        // Note: some units might not explode, but just lose hp
        player.ApplyDamage(1, Vector2.zero);
        StartCoroutine(Explode());
    }
}