using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerForward : MonoBehaviour
{
    public float forwardSpeed = 5f; // velocidade pra frente
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // calcula nova posição
        Vector3 newPosition = rb.position + Vector3.forward * forwardSpeed * Time.fixedDeltaTime;

        // aplica movimento
        rb.MovePosition(newPosition);
    }

    void Update()
    {
        
    }
}
