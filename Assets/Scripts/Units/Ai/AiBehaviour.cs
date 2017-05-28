

using UnityEngine;

public abstract class AiBehaviour : MovementInput {

    protected bool activated = false;
    public Transform initLocation;

    public bool ReceiveTrigger(GameObject other) {
        if (other.tag == "AiActivator") {
            if (GameplayManager.m.playerUnit != null && 
                !GameplayManager.m.playerUnit.isDead)
            ActivateAi();
            return true;
        }
        return false;
    }

    public void ActivateAi() {
        if (activated) return;
        InitAi();
        StartCoroutine(ai());
        activated = true;
    }

    protected virtual void InitAi() { }

    protected virtual System.Collections.IEnumerator ai() {
        yield return null;
    }
}

