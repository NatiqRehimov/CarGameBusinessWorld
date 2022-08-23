using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] Transform carPosition;
    [SerializeField] private float rotationSpeed;
    private Vector3 velocity = Vector3.zero;
    private void FixedUpdate()
    {
        TranslationAndRotation();
    }

    private void TranslationAndRotation()
    {
        //Translation
        transform.position = Vector3.SmoothDamp
            (transform.position, carPosition.TransformPoint(offset),ref velocity,rotationSpeed*Time.deltaTime);
        //Rotation
        var rotation = Quaternion.LookRotation(carPosition.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation,rotationSpeed * Time.deltaTime);
    }
}
