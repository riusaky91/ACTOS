using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform player1; // Referencia a P1
    public Transform player2; // Referencia a P2
    public float minZoom = 5f;  // Zoom mínimo cuando están cerca
    public float maxZoom = 10f; // Zoom máximo cuando están lejos
    public float smoothSpeed = 40f; // Suavidad del movimiento de la cámara

    private Camera cam;

    void Start()
    {
        cam = Camera.main; // Obtener la cámara principal
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        // 1. Calcular el punto medio entre los dos personajes
        Vector3 middlePoint = (player1.position + player2.position) / 2f;
        transform.position = Vector3.Lerp(transform.position, new Vector3(middlePoint.x, middlePoint.y + 2, transform.position.z), smoothSpeed * Time.deltaTime);

        // 2. Calcular la distancia entre los personajes
        float distance = Vector3.Distance(player1.position, player2.position);

        // 3. Ajustar el zoom de la cámara basado en la distancia
        float targetZoom = Mathf.Lerp(minZoom, maxZoom, distance / 10f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, smoothSpeed * Time.deltaTime);
    }
}
