using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Material material;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Renderer>().material = material;
            Destroy(gameObject, time);
        }
    }

}
