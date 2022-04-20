using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0.2f, 0.0f, -10);
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] float dampingTime = 0.3f;

    private void Update()
    {
        CameraMove();
    }

    void CameraMove()
    {
        Vector3 destination = new Vector3(target.position.x - offset.x, 
                                          target.position.y - offset.y, 
                                          offset.z);
        
        transform.position = Vector3.SmoothDamp(transform.position, 
                                                destination, 
                                                ref velocity, 
                                                dampingTime);
    }
}
