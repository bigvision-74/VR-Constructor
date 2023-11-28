using UnityEngine;
using System.Collections;

public class TransparentObjectController : MonoBehaviour
{
    private bool isTransparent = false;
    private Material material;
    private Color originalColor;

    void Start()
    {
        // Assuming your 3D model has a material with the Lit shader in URP
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        originalColor = material.color;
    }

    void Update()
    {
        // You can remove this if condition if you want transparency toggle on every click
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleTransparency();
        }
    }

    void ToggleTransparency()
    {
        float currentAlpha = material.color.a;
        float targetAlpha = isTransparent ? 1f : 0.3f; // Set target alpha based on the current state
        Debug.Log("target alpha: " + targetAlpha);

        // Choose the appropriate coroutine based on the transition direction
        if (isTransparent)
        {
            StartCoroutine(FadeToOpaque(currentAlpha, targetAlpha, 2f));
        }
        else
        {
            StartCoroutine(FadeToTransparent(currentAlpha, targetAlpha, 2f));
        }

        isTransparent = !isTransparent;
    }

    IEnumerator FadeToOpaque(float startAlpha, float targetAlpha, float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float percentage = (Time.time - startTime) / duration;
            Color newColor = material.color;
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, percentage);
            material.color = newColor;

            Debug.Log("Fading to Opaque: Alpha = " + newColor.a); // Debug statement

            yield return null;
        }

        Color finalColor = material.color;
        finalColor.a = targetAlpha;
        material.color = finalColor;
    }

    IEnumerator FadeToTransparent(float startAlpha, float targetAlpha, float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float percentage = (Time.time - startTime) / duration;
            Color newColor = material.color;
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, percentage);
            material.color = newColor;

            Debug.Log("Fading to Transparent: Alpha = " + newColor.a); // Debug statement

            yield return null;
        }

        Color finalColor = material.color;
        finalColor.a = targetAlpha;
        material.color = finalColor;
    }
}
