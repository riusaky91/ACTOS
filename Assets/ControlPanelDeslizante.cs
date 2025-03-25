using UnityEngine;

public class ControlPanelDeslizante : MonoBehaviour
{
    public Animator panelAnimator; // Referencia al componente Animator del panel

    public void MostrarPanel()
    {
        panelAnimator.SetTrigger("DeslizarAbajo"); // Activa el trigger de la animación
    }

    public void OcultarPanel()
    {
        panelAnimator.SetTrigger("OcultarGuia");
    }
}