using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform tarjet;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;



    void FixedUpdate()
    {
        Vector3 desiredPosition = tarjet.position + offset;
        Vector3 soothedPosiotion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = soothedPosiotion;   
    }    
}
