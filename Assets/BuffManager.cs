using UnityEngine;
using System.Collections;

public class ImmunityBuff : MonoBehaviour
{
    public float buffDuration = 5f;
    public AudioClip buffSound; // som do power-up
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Buff de imunidade coletado!");

            // Toca o som
            if (buffSound != null)
                audioSource.PlayOneShot(buffSound);

            // Ativa a imunidade no Player
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AtivarImunidade(buffDuration);
            }

            // Destrói o buff
            Destroy(gameObject, 0.1f); // espera 0.1s para o som tocar
        }
    }

    void Update()
    {
        // Gira o buff como a moeda
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }
}
