using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;    // velocidade pra frente
    public float laneOffset = 2f;      // distância das faixas
    public float laneChangeSpeed = 10f; // suavidade da troca de faixa

    private Rigidbody rb;
    private bool onRightLane = false;  // começa na esquerda

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 1️⃣ Movimento para frente
        Vector3 velocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardSpeed);
        rb.linearVelocity = velocity;

        // 2️⃣ Movimento lateral suave
        float targetX = onRightLane ? laneOffset : -laneOffset;
        Vector3 targetPosition = new Vector3(targetX, rb.position.y, rb.position.z);
        Vector3 newPosition = Vector3.Lerp(rb.position, targetPosition, laneChangeSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    void Update()
    {
        // 3️⃣ Detectar toque na tela ou clique
        if (Input.GetMouseButtonDown(0))
        {
            onRightLane = !onRightLane; // alterna faixa
        }

        // Opcional: aqui você pode adicionar swipe para celular
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Bateu no obstáculo!");
            Destroy(gameObject); // personagem some
        }
    }
}
