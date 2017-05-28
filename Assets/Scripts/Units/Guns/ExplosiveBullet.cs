using UnityEngine;
using System.Collections;

public class ExplosiveBullet : Bullet {

    public Transform explosionParticles;
    public float explosionRange = 2;

    protected override void ApplyDamage(Unit other) {
        // Apply damage in area instead of single target
        Transform t = Instantiate(explosionParticles);
        t.position = transform.position;
        Destroy(t.gameObject, 3);
        // Rectangular explosion check instead of circular.
        Collider2D[] cols = Physics2D.OverlapAreaAll(
            (Vector2)transform.position - new Vector2(explosionRange / 2, explosionRange / 2),
            (Vector2)transform.position + new Vector2(explosionRange/2, explosionRange / 2),
            1<<LayerMask.NameToLayer("EnemyCollision"));

        bool anyDamageApplied = false;
        for (int i = 0; i < cols.Length; i++) {
            UnitCollisionReceiver uniti = cols[i].GetComponent<UnitCollisionReceiver>();
            if (uniti) {
                anyDamageApplied = true;
                uniti.collisionTarget.ApplyDamage(damage, other.transform.position - transform.position);
            }
        }
        if (anyDamageApplied)
            Destroy(gameObject);
        else Debug.Log("No damage applied");
    }
}
