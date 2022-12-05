using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float transitionSpeed;
    [SerializeField] private float cameraSpeed;
   
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;


    private void Update()
    {
        transform.position = Vector3.SmoothDamp(
             transform.position,
             new Vector3(currentPosX, transform.position.y, transform.position.z),
             ref velocity,
             transitionSpeed);

    }

    public void MoveToNewRoom(Transform newRoom)
    {
        currentPosX = newRoom.position.x;    
    }
}
