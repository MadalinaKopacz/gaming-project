using UnityEngine;

public class MoveHorizontal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    private bool turnBack;
    

    void Update()
    {
        if(transform.position.x <= pointA.position.x)
            turnBack = true;

        if (transform.position.x >= pointB.position.x)
            turnBack = false;

        if (turnBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
    }
}
