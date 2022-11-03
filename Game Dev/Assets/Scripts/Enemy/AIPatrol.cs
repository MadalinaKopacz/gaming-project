using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;

    public Transform player;
    public float agroRange;
    public float moveSpeed;
    public GameObject alert;
    private Rigidbody2D rb;


    private void Reset()
    {
        Init();
    }

    void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        GameObject root = new GameObject(name + "_Root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = root.transform.position;

        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < agroRange)
        {
            ChasePlayer();
        } 
        else
        {
            MoveToNextPoint();
        }
    }

    private void MoveToNextPoint()
    {
        alert.SetActive(false);
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector2(-4, 4);
        else
            transform.localScale = new Vector2(4, 4);

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            if (nextID == 0)
                idChangeValue = 1;
            nextID += idChangeValue;
        }
    }

    private void ChasePlayer()
    {
        alert.SetActive(true);
        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-4, 4);
        }
        else if (transform.position.x > player.position.x)
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(4, 4);
        }
    }
}
