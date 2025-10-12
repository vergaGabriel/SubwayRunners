using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSound; // som da moeda
    private AudioSource audioSource;

    void Start()
    {
        // adiciona um AudioSource na moeda
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // quando o Player encostar
        {
            Debug.Log("Pegou moeda!");
            GameManager.instance.AddCoin();
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // gira a moeda
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }
}
