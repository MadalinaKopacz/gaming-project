using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && transform.position.y >= collision.transform.position.y)
        {
            Debug.Log($"Player y {transform.position.y}, platform y {collision.transform.position.y}");
            transform.parent = collision.gameObject.transform;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            transform.parent = null;
    }
}
