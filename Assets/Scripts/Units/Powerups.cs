using UnityEngine;
using System.Collections;
using System;

public class Powerups : MonoBehaviour {
    // speed powerups: normal -> speed up -> flash enabled
    // attack powerups: normal -> incrased bullet size with small aoe -> more firepower(double guns/support units fire)
    // todo: timeout: reduce by 1 level, gives a sense of pilot being on adrenalyne

    public Unit targetUnit;

    internal SpeedStates speedState;
    internal AttackStates attackState;

    float normalSpeed;
    public float fasterSpeed;
    public enum SpeedStates {
        Normal,
        Faster,
        Flash
    }

    public enum AttackStates {
        Normal,
        BiggerBullets,
        MoreBullets
    }

    public bool consumesPowerups;

    void Start() {
        normalSpeed = targetUnit.input.movement.speed;
        fasterSpeed = Mathf.Max(fasterSpeed, normalSpeed);

    }

    void OnTriggerEnter2D(Collider2D other) {
        ReceiveTrigger(other.gameObject);
    }

    public void ReceiveTrigger(GameObject other) {
        Unit otherUnit = other.GetComponent<Unit>();
        // powerups activate pickup on player
        if (targetUnit.alliance == "player") {
            if (otherUnit && otherUnit.alliance == "powerup") {
                GetPowerup(otherUnit.specialTag);// give player pickup
                Destroy(otherUnit.gameObject);
            }
        }
    }

    private void GetPowerup(string newPower) {
        if (!consumesPowerups) return;
        if (newPower == "speed") {
            IncreaseSpeed();
        } else
        if (newPower == "attack") {
            IncreaseAttack();
        }
    }

    void SetSpeed(SpeedStates states) {
        // Note that for now, target unit is always player.
        // This kind of script directly supports state changes by ai.
        switch (states) {
            case SpeedStates.Normal:
                targetUnit.input.gunLocked = true;
                targetUnit.input.movement.speed = normalSpeed;
                targetUnit.input.movement.sideSpeed = normalSpeed;
                targetUnit.input.jumpEnabled = false;
                break;
            case SpeedStates.Faster:
                targetUnit.input.gunLocked = true;
                targetUnit.input.movement.speed = fasterSpeed;
                targetUnit.input.movement.sideSpeed = normalSpeed;
                targetUnit.input.jumpEnabled = false;
                break;
            case SpeedStates.Flash:
                targetUnit.input.gunLocked = true;
                targetUnit.input.movement.speed = fasterSpeed;
                targetUnit.input.movement.sideSpeed = normalSpeed;
                targetUnit.input.jumpEnabled = true;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Assumes that target unit is player.
    /// </summary>
    /// <param name="attackState"></param>
    void SetAttack(AttackStates attackState) {
        switch (attackState) {
            case AttackStates.Normal:
                ((PlayerInput)(targetUnit.input)).guns.SmallerBullets();
                ((PlayerInput)(targetUnit.input)).guns.SupportFire(false);
                break;
            case AttackStates.BiggerBullets:
                ((PlayerInput)(targetUnit.input)).guns.BiggerBullets();
                ((PlayerInput)(targetUnit.input)).guns.SupportFire(false);
                break;
            case AttackStates.MoreBullets:
                ((PlayerInput)(targetUnit.input)).guns.BiggerBullets();
                ((PlayerInput)(targetUnit.input)).guns.SupportFire(true);
                break;
            default:
                break;
        }
    }

    internal void IncreaseSpeed() {
        if (speedState != SpeedStates.Flash) {
            Debug.Log("Speed upgrade");
            speedState = speedState + 1;
            attackState = AttackStates.Normal;
            SetSpeed(speedState);
            SetAttack(attackState);
        }
    }

    internal void IncreaseAttack() {
        if (attackState != AttackStates.MoreBullets) {
            Debug.Log("Attack upgrade");
            speedState = SpeedStates.Normal;
            attackState += 1;
            SetSpeed(SpeedStates.Normal);
            SetAttack(attackState);
        }
    }
}
