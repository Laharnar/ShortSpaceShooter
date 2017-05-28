using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base class for movement, contains functions for movement and rotation.
// Requires rigidbody.
/// </summary>
public class Movement : MonoBehaviour {

    // limit speed between these 2 values
    public float speed;
    public float sideSpeed;

    public Rigidbody2D rigbody;

    // Use this for initialization
    void Awake () {
        if (rigbody == null) rigbody = GetComponent<Rigidbody2D>();
        if (speed == 0) Debug.LogWarning("Speed is 0", this);
    }
    
    internal void Move(float verticalInput, float horizontalInput) {
        // Get direction from input and move around
        Vector2 verticalDir = (Vector2)(transform.up * verticalInput * Time.deltaTime * speed);
        Vector2 horizontalDir = (Vector2)(transform.right * horizontalInput * Time.deltaTime * sideSpeed);
        Vector2 dir = horizontalDir+verticalDir;
        rigbody.MovePosition(rigbody.position + dir);
    }

    internal void MoveTowards(Vector2 dir) {
        rigbody.MovePosition(rigbody.position + dir);
    }

    internal void Spin(Vector2 dir) {
        rigbody.MoveRotation(Vector3.Angle(transform.position-transform.forward, dir));
    }
}
