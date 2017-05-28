using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage = 1;

    public string alliance;

    public bool destroyOnNextHit = true;

    public Movement movement;

	// Use this for initialization
	void Start () {
        if (damage == 0) Debug.LogWarning("Set damage", this);
        if (alliance == "") Debug.LogWarning("Set alliance", this);
        if (movement == null) Debug.LogWarning("Set movement", this);
    }
	
    void Update() {
        movement.Move(1, 0);
    }

	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D other) {
        if (other.transform.name.Contains("Wall")) {
            Destroy(gameObject);
            return;
        }
        // Apply damage, if it's an enemy(by alliance).
        Unit otherUnit = other.gameObject.GetComponent<Unit>();
        if (otherUnit && alliance != otherUnit.alliance) {
            ApplyDamage(otherUnit);
        }
    }

    protected virtual void ApplyDamage(Unit otherUnit) {
        
            otherUnit.ApplyDamage(damage, otherUnit.transform.position - transform.position);

            // TODO: uncomment and set to false if you want to use piercing ammo(lasers)
            //destroyOnNextHit = true;
            //if (destroyOnNextHit)
            Destroy(gameObject);
    }
}
