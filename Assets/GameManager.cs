using UnityEngine;
using TMPro; // necessário para TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coinCount = 0;
    public TextMeshProUGUI coinText;

    void Start()
    {
        
    }

    void Awake()
    {
        // Singleton (garante que só existe 1 GameManager)
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        coinCount++;
        coinText.text = "Moedas: " + coinCount;
    }

    void Update()
    {
        
    }
}
