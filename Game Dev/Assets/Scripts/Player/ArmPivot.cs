using System;
using UnityEngine;

public class ArmPivot : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);

        if (rotation < -90f || rotation > 90f)
        {
            
            if (player.transform.eulerAngles.y <= 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotation);
            }
            else if (player.transform.eulerAngles.y >= 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotation);
            }
        }

        
    }
}
