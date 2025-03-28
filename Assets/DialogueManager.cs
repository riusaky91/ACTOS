using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;        // Texto del diálogo
    public TextMeshProUGUI characterName;      // Nombre del personaje
    public Image activeCharacterImage;         // Imagen del personaje activo (hablando)
    public Image passiveCharacterImage;        // Imagen del personaje pasivo (no hablando)
    public GameObject dialogueCanvas;          // Canvas que contiene los diálogos
    public Button nextButton;                  // Botón para avanzar al siguiente diálogo
    public float textSpeed = 0.05f;            // Velocidad de aparición del texto

    [System.Serializable]
    public class Dialogue
    {
        public string name;                     // Nombre del personaje
        public Sprite activeImage;              // Imagen del personaje activo
        public Sprite passiveImage;             // Imagen del personaje pasivo
        public string text;                     // Texto del diálogo
    }

    public List<Dialogue> dialogues;           // Lista de diálogos
    private int currentDialogueIndex = 0;      // Índice actual del diálogo
    private Coroutine typingCoroutine;         // Referencia para controlar el texto por carácter

    void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonClicked); // Asignar el botón para avanzar
        ShowDialogue(); // Mostrar el primer diálogo
    }

    public void ShowDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            Dialogue currentDialogue = dialogues[currentDialogueIndex];
            characterName.text = currentDialogue.name;

            // Cambiar las imágenes del personaje activo y pasivo
            activeCharacterImage.sprite = currentDialogue.activeImage;
            passiveCharacterImage.sprite = currentDialogue.passiveImage;

            // Detener cualquier aparición previa de texto y comenzar a mostrar el nuevo texto
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(currentDialogue.text));

            // Aplicar efectos visuales
            ApplyActiveEffects();
            ApplyPassiveEffects();
        }
        else
        {
            EndDialogues(); // Finalizar los diálogos cuando termine la lista
        }
    }

    IEnumerator TypeText(string text)
    {
        dialogueText.text = ""; // Vaciar el texto inicial
        foreach (char c in text.ToCharArray())
        {
            dialogueText.text += c; // Mostrar cada carácter progresivamente
            yield return new WaitForSeconds(textSpeed); // Esperar según la velocidad establecida
        }
    }

    public void OnNextButtonClicked()
    {
        if (typingCoroutine != null && dialogueText.text != dialogues[currentDialogueIndex].text)
        {
            // Completar el texto instantáneamente si aún se está mostrando por carácter
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogues[currentDialogueIndex].text;
        }
        else
        {
            // Avanzar al siguiente diálogo
            NextDialogue();
        }
    }

    public void NextDialogue()
    {
        currentDialogueIndex++;
        ShowDialogue();
    }

    public void EndDialogues()
    {
        dialogueCanvas.SetActive(false); // Ocultar el Canvas al finalizar
        Time.timeScale = 1;              // Reanudar el juego si estaba pausado
    }

    void ApplyActiveEffects()
    {
        // Efectos para el personaje activo (restaurar escala y opacidad)
        activeCharacterImage.color = new Color(1, 1, 1, 1); // Opacidad total
        activeCharacterImage.transform.localScale = Vector3.one; // Escala original
    }

    void ApplyPassiveEffects()
    {
        // Reducir opacidad y tamaño para el personaje pasivo
        passiveCharacterImage.color = new Color(0f, 0.5f, 0.5f, 1f); // Semitransparente
        passiveCharacterImage.transform.localScale = Vector3.one * 0.5f; // Reducir tamaño

        // Simular desenfoque si tiene un material con efecto Blur
        Material blurMaterial = passiveCharacterImage.material;
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurAmount", 4.5f); // Ajustar desenfoque
        }
    }
}