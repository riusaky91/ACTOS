using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 touchStart;
    private bool isPlayerControlled;
    private float limitMovement = 4f; // Límite de movimiento

    void Start()
    {
        // Solo P1 tendrá control del jugador
        isPlayerControlled = gameObject.name == "P1";
    }

    void Update()
    {
    if (!isPlayerControlled) return;

    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            touchStart = touch.position;

        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 direction = touch.position - touchStart;
            float moveX = direction.x > 0 ? 1 : -1;

            // Aplica límites al movimiento (ajusta según tu escenario)
            float newX = transform.position.x + moveX * speed * Time.deltaTime;
            newX = Mathf.Clamp(newX, limitMovement, limitMovement); // Limite de escenario

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}
}
