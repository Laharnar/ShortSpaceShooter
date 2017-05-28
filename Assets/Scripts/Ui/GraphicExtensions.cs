using UnityEngine;
using UnityEngine.UI;

public static class GraphicExtensions {
    // source: http://gamedev.stackexchange.com/questions/123938/unity-i-want-to-make-my-ui-text-fade-in-after-5-seconds

    /// <summary>
    /// Fade methods for UI elements;
    /// </summary>
    /// <param name="g"></param>
    public static void FadeIn(this Graphic g, float time = 1) {
        g.GetComponent<CanvasRenderer>().SetAlpha(0f);
        g.CrossFadeAlpha(1f, time, false);//second param is the time
    }

    public static void FadeOut(this Graphic g, float time = 1) {
        g.GetComponent<CanvasRenderer>().SetAlpha(1f);
        g.CrossFadeAlpha(0f, time, false);
    }

}