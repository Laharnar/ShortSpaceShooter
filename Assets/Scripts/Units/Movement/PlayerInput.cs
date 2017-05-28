using UnityEngine;

/// <summary>
/// Player's gameplay input, move and shoot.
/// 
/// Menu logic is handled in gameplay manager.
/// </summary>
public class PlayerInput : MovementInput {
    
    public Guns guns;
    
    void Awake() {
        gunLocked = true;
        if (movement == null) { movement = GetComponent<Movement>(); Debug.LogWarning("Assign movement manualy."); }
        if (guns == null) { guns = GetComponent<Guns>(); Debug.LogWarning("Assign gun manualy."); }
    }

    // Update is called once per frame
    void Update () {
        horizontalInput = Input.GetAxis("Horizontal");// left and right input
        verticalInput = Input.GetAxis("Vertical"); // back and forward

        movement.Move(verticalInput, horizontalInput);

        if (/*gunLocked || */Input.GetKey(KeyCode.Space)) {
            guns.Fire();
        }
    }

}

