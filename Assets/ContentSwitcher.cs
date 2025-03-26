using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

public class ContentSwitcher : MonoBehaviour
{
    public GameObject contentA; // Referencia al primer contenido
    public GameObject contentB; // Referencia al segundo contenido
    public GameObject botonA;  // GameObject del botón A
    public GameObject botonB;  // GameObject del botón B

    // Método que muestra ContentA y oculta ContentB
    public void ShowContentA()
    {
        contentA.SetActive(true);  // Activa el contenido A
        contentB.SetActive(false); // Desactiva el contenido B

        botonA.SetActive(false);   // Desactiva el GameObject del botón A
        botonB.SetActive(true);    // Activa el GameObject del botón B
    }

    // Método que muestra ContentB y oculta ContentA
    public void ShowContentB()
    {
        contentA.SetActive(false); // Desactiva el contenido A
        contentB.SetActive(true);  // Activa el contenido B

        botonA.SetActive(true);    // Activa el GameObject del botón A
        botonB.SetActive(false);   // Desactiva el GameObject del botón B
    }
}