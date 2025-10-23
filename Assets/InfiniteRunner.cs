using UnityEngine;
using System.Collections.Generic;

/*public class InfiniteRunner : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public GameObject chunkPrefab;

    [Header("Configurações")]
    public int activeChunks = 2;
    public float triggerDistance = 120f; // aumente para evitar gaps

    private List<Transform> chunks = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < activeChunks; i++)
        {
            SpawnChunk(i == 0 ? null : chunks[i - 1]);
        }
    }

    void Update()
    {
        if (chunks.Count == 0) return;

        Transform lastChunk = chunks[chunks.Count - 1];
        Transform endPoint = lastChunk.Find("EndPoint");

        if (endPoint == null)
        {
            Debug.LogError("EndPoint não encontrado na última chunk!");
            return;
        }

        float distanceToEnd = endPoint.position.z - player.position.z;

        Debug.Log($"Distância até o fim: {distanceToEnd:F2}");
        Debug.Log($"Player pos: {player.position}");

        if (player.position.z >= endPoint.position.z - triggerDistance)
        {
            Debug.Log("➡️ Gerando novo chunk!");
            SpawnChunk(lastChunk);

            Transform oldChunk = chunks[0];
            chunks.RemoveAt(0);
            Destroy(oldChunk.gameObject, 1f);
        }
    }


    void SpawnChunk(Transform previousChunk)
    {
        GameObject newChunk = Instantiate(chunkPrefab);
        Transform startPoint = newChunk.transform.Find("StartPoint");

        if (previousChunk != null)
        {
            Transform prevEnd = previousChunk.Find("EndPoint");
            Vector3 offset = newChunk.transform.position - startPoint.position;
            newChunk.transform.position = prevEnd.position + offset;
        }

        chunks.Add(newChunk.transform);
    }
}
*/
public class InfiniteRunner : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public GameObject chunkPrefab;

    [Header("Configurações")]
    public int activeChunks = 2;       // quantas chunks queremos manter ativas
    public float triggerDistance = 120f;

    private List<Transform> chunks = new List<Transform>();

    void Start()
    {
        // Gera as primeiras chunks
        for (int i = 0; i < activeChunks; i++)
        {
            SpawnChunk(i == 0 ? null : chunks[i - 1]);
        }
    }

    void Update()
    {
        if (chunks.Count == 0) return;

        // Pegamos a última chunk
        Transform lastChunk = chunks[chunks.Count - 1];
        Transform endPoint = lastChunk.Find("EndPoint");

        if (endPoint == null)
        {
            Debug.LogError("EndPoint não encontrado na última chunk!");
            return;
        }

        float distanceToEnd = endPoint.position.z - player.position.z;

        // Se o jogador estiver perto do fim, spawn a próxima chunk
        if (distanceToEnd <= triggerDistance)
        {
            SpawnChunk(lastChunk);

            // Remove a primeira chunk se passarmos do limite de chunks ativas
            if (chunks.Count > activeChunks)
            {
                Transform oldChunk = chunks[0];
                chunks.RemoveAt(0);
                Destroy(oldChunk.gameObject, 1f);
            }
        }
    }

    void SpawnChunk(Transform previousChunk)
    {
        GameObject newChunk = Instantiate(chunkPrefab);
        Transform startPoint = newChunk.transform.Find("StartPoint");

        if (previousChunk != null && startPoint != null)
        {
            Transform prevEnd = previousChunk.Find("EndPoint");
            if (prevEnd != null)
            {
                Vector3 offset = newChunk.transform.position - startPoint.position;
                newChunk.transform.position = prevEnd.position + offset;
            }
        }

        chunks.Add(newChunk.transform);
    }
}