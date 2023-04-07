using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 offset = new Vector3(0, 0, 0);
    public float smoothTime = 0.2f;
    public Vector3 velocity = Vector3.zero;
    public Transform target;

    void Update()
    {
        Vector3 targetposition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);
    }
}
