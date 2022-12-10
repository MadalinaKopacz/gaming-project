using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    [SerializeField] private float yOffeset;
    [SerializeField] private float xOffeset;

    public Transform target;  // the player

    private void Start()
    {
        Vector3 newPos = new Vector3(target.position.x + 2*xOffeset, target.position.y + yOffeset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }


    private void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + xOffeset, target.position.y + yOffeset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);

    }
}
