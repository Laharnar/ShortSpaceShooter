using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Updates ui with health.
/// Expects n health.
/// </summary>
public class HealthUi : MonoBehaviour {

    public Transform[] healthUi;

    public Transform damagedUi;

    /// <summary>
    /// Show same number of bars as there is health left.
    /// </summary>
    public void SetUi(int currentHp) {
        if (currentHp > healthUi.Length) Debug.LogWarning("You don't have enough health bars.");
        for (int i = 0; i < healthUi.Length; i++) {
            if (healthUi[i] == null) Debug.LogWarning("Reference is null. "+name, this);
            healthUi[i].gameObject.SetActive(i < currentHp);
        }
        /*if (damagedUi)
            StartCoroutine(DisplayDamaged());*/
    }

    /*private IEnumerator DisplayDamaged() {
        damagedUi.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        damagedUi.gameObject.SetActive(false);

    }*/
}
