using UnityEngine;
using System.Collections;

/// <summary>
/// Self destructs on out of level.
/// Assumes hit object has parent with Limits in name.
/// </summary>
public class DestroyOutOfLevel : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.name.Contains("Wall")) {
            Destroy(gameObject);
        }
    }
}
