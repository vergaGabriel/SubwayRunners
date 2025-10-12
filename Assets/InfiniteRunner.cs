using UnityEngine;
using System.Collections.Generic;

public class InfiniteRunner : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public GameObject chunkPrefab;

    [Header("Configurações")]
    public int activeChunks = 2;
    public float triggerDistance = 60f; // aumente para evitar gaps

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
        }
        else
        {
            Debug.Log("EndPoint Z: " + endPoint.position.z);
        }

        if (player.position.z >= endPoint.position.z - triggerDistance)
        {
            SpawnChunk(lastChunk);

            // Remove o mais antigo com leve delay
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
