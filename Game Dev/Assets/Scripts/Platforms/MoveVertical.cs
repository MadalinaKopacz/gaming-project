using UnityEngine;

public class MoveVertical : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    private bool turnBack;


    void Update()
    {
        if (transform.position.y <= pointA.position.y)
            turnBack = true;

        if (transform.position.y >= pointB.position.y)
            turnBack = false;

        if (turnBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
    }
}
