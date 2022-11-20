using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerScript playerStats;

    public void Start()
    {
        playerStats = player.GetComponent<PlayerScript>();
    }

    public void resetGame()
    {
        resetStats();
        SceneManager.LoadScene(0);
    }

    private void resetStats()
    {
        playerStats.setHp(100);
    }
}
