using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowUIElementsAfterTime : MonoBehaviour
{
    public GameObject[] uiElements; // Asigna los botones o paneles desde el inspector
    public float delay = 5f; // Tiempo de espera en segundos
    public float fadeDuration = 1f; // Duración del fade in en segundos

    void Start()
    {
        foreach (GameObject element in uiElements)
        {
            element.SetActive(false); // Oculta todos los elementos al inicio
        }
        StartCoroutine(ShowElementsAfterDelay());
    }

    IEnumerator ShowElementsAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado

        foreach (GameObject element in uiElements)
        {
            element.SetActive(true); // Muestra el elemento
            StartCoroutine(FadeInElement(element));
        }
    }

    IEnumerator FadeInElement(GameObject element)
    {
        CanvasGroup canvasGroup = element.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = element.AddComponent<CanvasGroup>();
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