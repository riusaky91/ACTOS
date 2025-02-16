using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private float direction = 1f;
    private float limit = 2f; // Límite de movimiento

    void Update()
    {
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        if (transform.position.x > limit || transform.position.x < -limit)
        {
            direction *= -1; // Cambia de dirección cuando llega al límite
        }
    }
}