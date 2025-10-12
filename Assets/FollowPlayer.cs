using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;       // arraste o PlayerRoot aqui
    public Vector3 offset = new Vector3(0, 2, -4); // ajuste altura e distância

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if(player == null) return;

        // mantém a câmera atrás do player
        transform.position = player.position + offset;

        // olha sempre para o player
        transform.LookAt(player);
    }

    void Update()
    {
        
    }
}
