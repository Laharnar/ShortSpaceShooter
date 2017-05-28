using UnityEngine;
using System.Collections;

/// <summary>
/// Passes collion to target
/// </summary>
public class UnitCollisionReceiver : MonoBehaviour {

    public Unit collisionTarget;
    public AiBehaviour triggerTarget;

    public bool disableAfterUse = false;

    void Awake() {
        if (collisionTarget == null || triggerTarget == null) {
            Debug.LogWarning("Set targets manualy", this);
        }
    }

	void OnCollisionEnter2D (Collision2D other) {
        collisionTarget.ReceiveCollision(other.gameObject);

        if (disableAfterUse) {
            GetComponent<Collider2D>().enabled = false;
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (triggerTarget)
            if (triggerTarget.ReceiveTrigger(other.gameObject)) {
                if (disableAfterUse) {
                    GetComponent<Collider2D>().enabled = false;
                }
            }
    }
}
