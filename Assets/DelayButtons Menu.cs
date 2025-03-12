using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowButtonAfterTime : MonoBehaviour
{
    public Button myButton; // Asigna el botón desde el inspector
    public float delay = 5f; // Tiempo de espera en segundos
    public float fadeDuration = 1f; // Duración del fade in en segundos

    void Start()
    {
        myButton.gameObject.SetActive(false); // Oculta el botón al inicio
        StartCoroutine(ShowButtonAfterDelay());
    }

    IEnumerator ShowButtonAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado
        myButton.gameObject.SetActive(true); // Muestra el botón
        StartCoroutine(FadeInButton());
    }

    IEnumerator FadeInButton()
    {
        CanvasGroup canvasGroup = myButton.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = myButton.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f; // Inicialmente invisible

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // Completamente visible
    }
}