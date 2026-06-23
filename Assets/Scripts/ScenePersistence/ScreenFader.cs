using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public IEnumerator Fade(float start, float end, float duration)
    {
        float t = 0;
        while(t < duration)
        {
            t += Time.deltaTime;
            //set canvas alpha to proporcional value (t/duration) between start an end values
            canvasGroup.alpha = Mathf.Lerp(start, end, t/duration);
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}

