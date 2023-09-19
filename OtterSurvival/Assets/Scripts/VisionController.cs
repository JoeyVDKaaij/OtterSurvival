using System;
using System.Collections;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    [Header("Rotation Options")]
    [SerializeField, Tooltip("Check if this GameObject can rotate along the X angle")]
    private bool xRotation = false;
    [SerializeField, Tooltip("Check if this GameObject can rotate along the Y angle")]
    private bool yRotation = false;
    [SerializeField, Tooltip("Check if this GameObject can rotate along the Z angle")]
    private bool zRotation = false;

    [Header("Angles")]
    [SerializeField, Tooltip("X angle cannot be less than this amount")]
    private float minXAngle = -90f;
    [SerializeField, Tooltip("X angle cannot be more than this amount")]
    private float maxXAngle = 90f;
    [SerializeField, Tooltip("Y angle cannot be less than this amount")]
    private float minYAngle = -90f;
    [SerializeField, Tooltip("Y angle cannot be more than this amount")]
    private float maxYAngle = 90f;
    [SerializeField, Tooltip("Z angle cannot be less than this amount")]
    private float minZAngle = -90f;
    [SerializeField, Tooltip("Z angle cannot be more than this amount")]
    private float maxZAngle = 90f;

    [Header("Rotation Modifications")]
    [SerializeField, Tooltip("How fast the GameObject rotates along the X axis")]
    private float xRotationSpeed = 5;
    [SerializeField, Tooltip("How fast the GameObject rotates along the Y axis")]
    private float yRotationSpeed = 5;
    [SerializeField, Tooltip("How fast the GameObject rotates along the Z axis")]
    private float zRotationSpeed = 5;

    private float currentXAngle;
    private float currentYAngle;
    private float currentZAngle;

    private void Start()
    {
        currentXAngle = transform.rotation.x;
        currentYAngle = transform.rotation.y;
        currentZAngle = transform.rotation.z;
    }

    private void Update()
    {
        if (xRotation)
        {
            currentXAngle = Mathf.Lerp(minXAngle, maxXAngle, Mathf.PingPong(Time.time * xRotationSpeed, 1.0f));
        }
        if (yRotation)
        {
            currentYAngle = Mathf.Lerp(minYAngle, maxYAngle, Mathf.PingPong(Time.time * yRotationSpeed, 1.0f));
        }
        if (zRotation)
        {
            currentZAngle = Mathf.Lerp(minZAngle, maxZAngle, Mathf.PingPong(Time.time * zRotationSpeed, 1.0f));
        }

        transform.rotation = Quaternion.Euler(currentXAngle, currentYAngle, currentZAngle);
    }
}
