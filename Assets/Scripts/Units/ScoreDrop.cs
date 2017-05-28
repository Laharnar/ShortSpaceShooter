using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Show score when unit dies.
/// </summary>
public class ScoreDrop : MonoBehaviour {


    const string prefix = "+";
    public int dropAmount = 1;

    public Text scoreTextObj;
    const float dropTime = 0.5f;

	void Awake () {
        if (scoreTextObj)
            scoreTextObj.gameObject.SetActive(false);   
    }

    public void Drop () {
        GameplayManager.AddScore(dropAmount);
        if (scoreTextObj) {
            scoreTextObj.text = prefix + dropAmount;
            scoreTextObj.gameObject.SetActive(true);
            scoreTextObj.FadeOut(dropTime);
        }
    }
}
