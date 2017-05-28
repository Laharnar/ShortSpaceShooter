using UnityEngine;

/// <summary>
/// Default ai movement behaviour. Moves forward on predefined or random path.
/// 
/// </summary>
public class AiInput : AiBehaviour {

    // use for targeting
    //GameObject playerTarget;

    public bool choseRandomPath = false;

    // 1: down, -1: up, in which direction to move forward
    public int direction = 1;

    /// <summary>
    /// You can use this to form formations with random paths.
    /// - set a parent of formation as formation target to single unit(leader)
    /// - set choose random path to true on leader
    /// - disable choose random path on every other ship in formation
    /// </summary>
    public Transform formationTarget;

    public bool isRandomized = false;

    public ActivatedGuns guns;

    public bool disableAfterInit = false;

    void Awake() {
        //playerTarget = GameObject.FindGameObjectWithTag("Player");

        if (movement == null) Debug.LogWarning("Set movement", this);
    }

    protected override void InitAi() {
        if (activated) return;
        if (choseRandomPath) {
            if (!formationTarget) 
                formationTarget = transform;
            formationTarget.position = PathManager.ChosePath(formationTarget.position);
        } else {
            // move to init location on x axis
            if (!isRandomized)
            transform.position = new Vector2(initLocation.position.x, transform.position.y);
        }
        if (guns)
            guns.Activate(0);
        if (disableAfterInit) {
            enabled = false;
        }
    }

    void Update() {
        // default ai movement is forward
        verticalInput = direction;
        movement.Move(verticalInput, 0);

        //movement.Rotate(horizontalInput);
    }
}

[System.Obsolete("not used")]
public static class RaycastHelper {
    /// <summary>
    /// Fires raycast from some object.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="relativeDir">Direction relative to transform.</param>
    /// <param name="distance"></param>
    /// <param name="layerMaskIndex">Non shifted layer index.</param>
    /// <returns></returns>
    public static RaycastHit2D RaycastInDirection(this Transform transform, Vector2 relativeDir, float distance, int layerMaskIndex) {
        return Physics2D.Raycast(transform.position, relativeDir, distance, 1<<layerMaskIndex);
    }

    public static RaycastHit2D RaycastInDirection(this Transform transform, Vector2 relativeDir, float distance) {
        return Physics2D.Raycast(transform.position, relativeDir, distance);
    }


    /*
      RaycastHit2D[] raycasts = FiveRaycasts();
      for (int i = 0; i < raycasts.Length; i++) {
          if (raycasts[i].transform == playerTarget) {
              horizontalInput = weights[i] * Mathf.Clamp(Vector3.Dot(playerTarget.position - transform.position,
                  transform.right), -1, 1);//1
          }
      }*/

    [System.Obsolete("Use it if you need direction based AI.")]
    public static RaycastHit2D[] FiveRaycasts(this Transform transform) {

        RaycastHit2D[] r = new RaycastHit2D[5];
        // Raycasts in order: forward, far right, far left, near right, near left
        Vector3[] dirs = new Vector3[5] {
            transform.up,
            (transform.right).normalized,
            (-transform.right).normalized,
            (transform.up+transform.right/1.25f).normalized,
            (transform.up-transform.right/1.25f).normalized
        };
        float[] rayLen = new float[5] {
            30,
            5,
            5,
            10,
            10
        };

        // might change distances later
        for (int i = 0; i < rayLen.Length; i++) {
            r[i] = transform.RaycastInDirection(dirs[i], rayLen[i]);
            Debug.DrawRay(transform.position, dirs[i] * rayLen[i]);
        }
        return r;
    }
}
