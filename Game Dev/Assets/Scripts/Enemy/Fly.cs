using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{

    [SerializeField] private int hp = 10;

    [SerializeField] private List<Transform> points;
    [SerializeField] private int nextID = 0;
    private int idChangeValue = 1;


    [SerializeField] private float moveSpeed;
    [SerializeField] private int scale = 4;


    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletParent;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime;

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

        points = new List<Transform> { p1.transform, p2.transform };
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveToNextPoint();
        if (nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = scale * new Vector2(-1, 1);
        else
            transform.localScale = scale * new Vector2(1, 1);

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


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            //print(1);

            // Get damage per hit from player
            GameObject player = GameObject.Find("Player");
            int damage = player.GetComponent<PlayerScript>().DamagePerHit;
            hp -= damage;

            if (hp <= 0)
            {
                //Destroy(gameObject);
                Destroy(transform.parent.gameObject);
            }
        }
    }

}
