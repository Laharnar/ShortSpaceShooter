using UnityEngine;
using System.Collections;

public class PingPongMovement : Movement{

    float movingDir = 0;
    bool canReset = true;

	// Use this for initialization
	void Start () {
        movingDir = 1;
	}
	
	// Update is called once per frame
	void Update () {
        /* if (5 < Mathf.Abs(((Vector2)transform.position + Vector2.right * movingDir * pingPongSpeed).x)){
             movingDir *= -1;
         }*/

        Move(speed, movingDir);
        
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (canReset && other.transform.tag == "Boss_walls") {
            movingDir *= -1;
            canReset = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.transform.tag == "Boss_walls") {
            canReset = true;
        }
    }
}
