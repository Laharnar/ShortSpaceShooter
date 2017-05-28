
/// <summary>
/// Base for input
/// </summary>
public abstract class MovementInput : UnityEngine.MonoBehaviour {

    public Movement movement;
    internal bool gunLocked = false;
    internal bool jumpEnabled = false;

    // 0-1 value for acceleration/decceleration and left/right rotation
    public float horizontalInput { get; protected set; }
    public float verticalInput { get; protected set; }

}
