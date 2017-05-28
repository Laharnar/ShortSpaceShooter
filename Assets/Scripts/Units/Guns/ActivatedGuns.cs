using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Control which guns can fire, and fire them
/// </summary>
public class ActivatedGuns : MonoBehaviour {

    public Guns[] guns;
    bool[] canFire;

    void Awake() {
        canFire = new bool[guns.Length];
    }

    void Update() {
        for (int i = 0; i < guns.Length; i++) {
            if (canFire[i] == true && guns[i] != null)
                guns[i].Fire();
        }
    }

    internal void Activate(int index) {
        canFire[index] = true;
    }

    internal void Deactivate(int index) {
        canFire[index] = false;
    }
}
