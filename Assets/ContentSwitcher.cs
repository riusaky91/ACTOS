using UnityEngine;
using UnityEngine.UI;

public class ContentSwitcher : MonoBehaviour
{
    public GameObject contentA; // Referencia al primer content
    public GameObject contentB; // Referencia al segundo content
    public Button botonA; // Botón para mostrar ContentA
    public Button botonB; // Botón para mostrar ContentB

    // Método que muestra ContentA y oculta ContentB
    public void ShowContentA()
    {
        contentA.SetActive(true);
        contentB.SetActive(false);
        botonA.interactable = false; // Desactiva el botón A
        botonB.interactable = true;  // Activa el botón B
    }

    // Método que muestra ContentB y oculta ContentA
    public void ShowContentB()
    {
        contentA.SetActive(false);
        contentB.SetActive(true);
        botonA.interactable = true;  // Activa el botón A
        botonB.interactable = false; // Desactiva el botón B
    }
}
