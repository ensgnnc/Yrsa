using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PropsAltar : MonoBehaviour {
    public List<SpriteRenderer> runes;
    public List<Light2D> lights;

    public float lerpSpeed;

    private Color curColor;
    private Color targetColor;

    private Color curLightColor;
    private Color targetLightColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            targetColor = new Color(1, 1, 1, 1);
            targetLightColor = new Color(32, 134, 183, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        targetColor = new Color(1, 1, 1, 0);
        targetLightColor = new Color(32, 134, 183, 0);
    }

    private void Update() {
        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);
        curLightColor = Color.Lerp(curColor, targetLightColor, lerpSpeed * Time.deltaTime);

        foreach (var r in runes)
        {
            r.color = curColor;
        }

        foreach (var l in lights)
        {
            l.color = curLightColor;
        }
    }
}
