using UnityEngine;
using System.Collections;

public class FreeRotation : MonoBehaviour {

    public float speed = 10;

	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, speed, Space.World);

    }
}
