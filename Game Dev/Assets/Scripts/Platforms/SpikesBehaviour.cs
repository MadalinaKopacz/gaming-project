using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject healthBar;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerStats = player.GetComponent<PlayerScript>();
            var healthBarActions = healthBar.GetComponent<HealthBarScript>();
            playerStats.setHp(playerStats.getHp() - 5);
            healthBarActions.setHealth();
            playerStats.checkGameOver();
        }
    }
}
