using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
/*public class PlayerController : MonoBehaviour
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
}*/

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float forwardSpeed = 5f;
    public float laneOffset = 2f;
    public float laneChangeSpeed = 10f;

    [Header("Jump")]
    public float jumpForce = 5f;
    public float rayLength = 0.5f; // comprimento do ray para detectar chão
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool onRightLane = false;
    private bool isImmune = false;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // evita o boneco girar
    }

    void FixedUpdate()
    {

        // Movimento para frente (Z)
        Vector3 forwardMovement = new Vector3(0, 0, forwardSpeed * Time.fixedDeltaTime);

        // Movimento lateral suave
        float targetX = onRightLane ? laneOffset : -laneOffset;
        Vector3 lateralMovement = new Vector3(targetX - rb.position.x, 0, 0) * laneChangeSpeed * Time.fixedDeltaTime;

        // Move o Rigidbody
        rb.MovePosition(rb.position + forwardMovement + lateralMovement);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onRightLane = !onRightLane;
        }

        // Verifica se está no chão usando raycast
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayer);

        // Pulo
        if (Input.GetMouseButtonDown(1))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Bateu no obstáculo!");
            Destroy(gameObject);
        }
    }*/
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (!isImmune)
            {
                Debug.Log("Bateu no obstáculo!");
                Destroy(gameObject);
            }
            
        }
    }

    public void AtivarImunidade(float duracao)
    {
        StopCoroutine(nameof(ImunidadeCoroutine));
        StartCoroutine(ImunidadeCoroutine(duracao));
    }

    private IEnumerator ImunidadeCoroutine(float duracao)
    {
        isImmune = true;
        yield return new WaitForSeconds(duracao);
        isImmune = false;
    }
}