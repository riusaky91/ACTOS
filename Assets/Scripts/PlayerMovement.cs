using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 150f; // Velocidad de respuesta
    private Vector2 touchStart;
    private bool isPlayerControlled;
    public float limit = 4f; // Límite de movimiento
    private Vector3 originalScale; // Tamaño original del personaje

    void Start()
    {
        // Solo P1 tendrá control del jugador
        isPlayerControlled = gameObject.name == "P1";
        originalScale = transform.localScale; // Guarda el tamaño original
    }

void Update()
    {
        if (!isPlayerControlled) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 direction = touch.position - touchStart;
                float moveX = direction.x * 0.01f; // Escala el movimiento para mayor sensibilidad
                float moveY = direction.y * 0.01f; // Detecta deslizamiento vertical

                // Movimiento horizontal con límites
                float newX = transform.position.x + moveX * speed * Time.deltaTime;
                newX = Mathf.Clamp(newX, -limit, limit);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);

                // Calcula el ángulo del deslizamiento
                float angle = Mathf.Atan2(direction.y, Mathf.Abs(direction.x)) * Mathf.Rad2Deg;

                // Si el ángulo es mayor a 45° y se mueve hacia abajo, reduce el tamaño
                if (angle < -45f)
                {
                    StopAllCoroutines(); // Cancela cualquier cambio de tamaño previo
                    StartCoroutine(ChangeSize(originalScale * 0.5f)); // Reduce al 50%
                }

                // Si el ángulo es mayor a 45° y se mueve hacia arriba, agranda el tamaño
                if (angle > 45f)
                {
                    StopAllCoroutines();
                    StartCoroutine(ChangeSize(originalScale * 1.5f)); // Aumenta 150%
                }

                touchStart = touch.position; // Actualiza para movimientos más fluidos
            }
        }
    }

    IEnumerator ChangeSize(Vector3 newSize)
    {
        transform.localScale = newSize; // Aplica el nuevo tamaño
        yield return new WaitForSeconds(0.5f); // Espera 2 segundos
        transform.localScale = originalScale; // Vuelve al tamaño original
    }
}
