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
    [SerializeField, Tooltip("Check if this GameObject should rotation is continues")]
    private bool continuesRotation = false;
    [SerializeField, Tooltip("Check if this GameObject should rotation is continues in reverse")]
    private bool continuesReverseRotation = false;

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
        Quaternion localRotation = transform.localRotation;

        currentXAngle = localRotation.eulerAngles.x;
        currentYAngle = localRotation.eulerAngles.y;
        currentZAngle = localRotation.eulerAngles.z;
    }

    private void Update()
    {
        if (!continuesRotation && !continuesReverseRotation)
        {
            NonContinues();
        }
        else if (continuesReverseRotation)
        {
            ReverseContinues();
        }
        else
        {
            Continues();
        }

        transform.localRotation = Quaternion.Euler(currentXAngle, currentYAngle, currentZAngle);
        
        
    }

    private void NonContinues()
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
    }

    private void Continues()
    {

        if (xRotation)
        {
            currentXAngle += xRotationSpeed;
            if (currentXAngle >= 360)
            {
                currentXAngle = 0 + (currentXAngle - 360);
            }
        }
        if (yRotation)
        {
            currentYAngle += yRotationSpeed;
            if (currentYAngle >= 360)
            {
                currentYAngle = 0 + (currentYAngle - 360);
            }
        }
        if (zRotation)
        {
            currentZAngle += zRotationSpeed;
            if (currentZAngle >= 360)
            {
                currentZAngle = 0 + (currentZAngle - 360);
            }
        }
    }

    private void ReverseContinues()
    {
        if (xRotation)
        {
            currentXAngle -= xRotationSpeed;
            if (currentXAngle <= 0)
            {
                currentXAngle = 360 + currentXAngle;
            }
        }
        if (yRotation)
        {
            currentYAngle -= yRotationSpeed;
            if (currentYAngle <= 0)
            {
                currentYAngle = 360 + currentYAngle;
            }
        }
        if (zRotation)
        {
            currentZAngle -= zRotationSpeed;
            if (currentZAngle <= 0)
            {
                currentZAngle = 360 + currentZAngle;
            }
        }
    }
}
