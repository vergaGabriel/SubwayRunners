using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float laneOffset = 2f;
    public float laneChangeSpeed = 10f;

    private Rigidbody rb;
    private bool onRightLane = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Movimento para frente (Z)
        Vector3 velocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardSpeed);
        rb.linearVelocity = velocity;

        Debug.Log($"Posição Z: {rb.position.z:F2}");

        // Movimento lateral suave
        float targetX = onRightLane ? laneOffset : -laneOffset;
        Vector3 targetPosition = new Vector3(targetX, rb.position.y, rb.position.z);
        Vector3 newPosition = Vector3.Lerp(rb.position, targetPosition, laneChangeSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onRightLane = !onRightLane;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Bateu no obstáculo!");
            Destroy(gameObject);
        }
    }
}
