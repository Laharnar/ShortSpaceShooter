using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

/// <summary>
/// Allows jumping between scenes.
/// </summary>
public class LevelManager : MonoBehaviour {

    public static LevelManager m { get; private set; }

    public Color fadeOutColor;
    public Color fadeInColor;

    public Image fadeOut;

    public bool loadOnAwake = true;

    void Awake() {
        m = this;

        if (loadOnAwake)
            StartCoroutine(FadeIn());

    }

    public void QuitApp() {
        Application.Quit();
    }


    private IEnumerator FadeIn() {
        fadeOut.enabled = true;
        fadeOut.CrossFadeColor(Color.clear, 1.75f, false, true);
        fadeOut.CrossFadeAlpha(0, 1.75f, false);

        yield return new WaitForSeconds(1.75f);
        fadeOut.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="choice">which level in the list will be loaded</param>
    public void LoadLevel(string sceneName) {
        Time.timeScale = 1;
        // TODO: first check if loading
        //StartCoroutine(FadeInLoad(choice));
        SceneManager.LoadScene(sceneName);

    }

    [System.Obsolete("TODO : fix buggy fadeout")]
    private IEnumerator FadeInLoad(int choice) {
        fadeOut.gameObject.SetActive(true);
        fadeOut.color = Color.clear;
        fadeOut.CrossFadeColor(fadeOutColor, 1f, false, true);
        fadeOut.CrossFadeAlpha(1, 1f, false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(choice);
    }
}
