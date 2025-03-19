using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneOnClick : MonoBehaviour
{
    public Button myButton; // Asigna el botón desde el inspector
    public string sceneName; // Nombre de la escena a la que quieres cambiar

    void Start()
    {
        myButton.onClick.AddListener(ChangeScene); // Agrega el listener al botón
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName); // Cambia a la escena especificada
    }
}