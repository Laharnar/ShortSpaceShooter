using UnityEngine;
using System.Collections;
using System;


public class BossShootAi:AiBehaviour{

    public Unit unit;

    public ActivatedGuns guns;


    protected override IEnumerator ai() {
        movement.speed = 1.5f;
                EnableMovement(true);
        yield return new WaitForSeconds(4.7f);
        movement.speed = 0;

        while (true) {

            if (unit.HealthBellowPercetage(0.25f)) {
                Debug.Log("Hp low");
                // different attack pattern, shoot rotating gun +  side or front gun randomly
                // bonus: spawn enemies and tp them to side
                EnableMovement(true);
                guns.Deactivate(0);
                guns.Activate(1);
                guns.Activate(2);
                guns.Activate(3);
                yield return new WaitForSeconds(4);
                guns.Activate(0);
                guns.Deactivate(1);
                guns.Activate(2);
                guns.Activate(3);
                yield return new WaitForSeconds(4);
                guns.Activate(0);
                guns.Activate(1);
                guns.Deactivate(2);
                guns.Activate(3);
                yield return new WaitForSeconds(4);
                guns.Activate(0);
                guns.Activate(1);
                guns.Activate(2);
                guns.Deactivate(3);
                yield return new WaitForSeconds(4);
            } else {
                // chose between 2 attack patterns
                int attackPattern = UnityEngine.Random.Range(0, 3) + 1;//Mathf.RoundToInt(Random.Range(0, 100)/100);
                if (attackPattern == 1) {
                    Debug.Log("Attack pattern 1");
                    // shoot side guns and move for a while, then stop and shoot rotating gun
                    EnableMovement(true);
                    guns.Deactivate(2);
                    guns.Deactivate(3);
                    guns.Activate(0);
                    guns.Activate(1);
                    yield return new WaitForSeconds(6);
                    EnableMovement(false);
                    guns.Deactivate(0);
                    guns.Deactivate(1);
                    guns.Activate(4);
                    yield return new WaitForSeconds(1);
                }
                else if (attackPattern == 2) {
                    Debug.Log("Attack pattern 2");
                    // shoot frontal gun and move
                    EnableMovement(true);
                    guns.Deactivate(0);
                    guns.Deactivate(1);
                    guns.Activate(2);
                    guns.Activate(3);
                    yield return new WaitForSeconds(5);
                    guns.Deactivate(2);
                    guns.Deactivate(3);
                    yield return new WaitForSeconds(1);
                } else if (attackPattern == 3) {
                    Debug.Log("Attack pattern 3");
                    // shoot side guns and front guns and move for a while
                    EnableMovement(true);
                    guns.Deactivate(4);
                    guns.Activate(0);
                    guns.Activate(1);
                    guns.Activate(2);
                    guns.Activate(3);
                    yield return new WaitForSeconds(4);
                    guns.Deactivate(0);
                    guns.Deactivate(1);
                    guns.Deactivate(2);
                    guns.Deactivate(3);
                    yield return new WaitForSeconds(2);

                }
            }
            yield return null;
        }
    }

    private void EnableMovement(bool v) {
        movement.enabled = v;
    }
}
