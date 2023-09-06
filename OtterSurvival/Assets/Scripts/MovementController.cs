using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

// This script requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float movementVelocityMultiplier = 5;
    [SerializeField]
    private float rotationSpeed = 10;
    private bool underWater = false;
    [SerializeField]
    private bool useForce = false;
    [SerializeField]
    private bool useTorque = false;

    Vector2 rotation;
    Vector2 currentRotation;
    [SerializeField]
    private float sensitivity = 2.0f;
    [SerializeField]
    private float minYRotation = -90f;
    [SerializeField]
    private float maxYRotation = 90f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Movements

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("Mouse X"));
        Debug.Log(Input.GetAxis("Mouse Y"));

        if (!useForce) rb.velocity = Vector3.zero;
        if (!useTorque) rb.angularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W) && !useForce)
        {
            rb.velocity = transform.forward * movementVelocityMultiplier;
        }
        if (Input.GetKey(KeyCode.S) && !useForce)
        {
            rb.velocity = -transform.forward * movementVelocityMultiplier;
        }

        rotation.x = Input.GetAxis("Mouse X");
        rotation.y = Input.GetAxis("Mouse Y");

        rotation *= sensitivity;

        currentRotation += rotation;

        currentRotation.y = Mathf.Clamp(currentRotation.y, minYRotation, maxYRotation);

        if (useTorque)
        {
            rb.AddTorque(new Vector3(rotation.y, rotation.x, 0) * sensitivity);
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(currentRotation.y, currentRotation.x, 0));
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && useForce)
        {
            //rb.AddRelativeForce(transform.forward * movementVelocityMultiplier);
            rb.AddForce(transform.forward * movementVelocityMultiplier);
        }
        if (Input.GetKey(KeyCode.S) && useForce)
        {
            rb.AddForce(-transform.forward * movementVelocityMultiplier);
        }
        if (Input.GetKey(KeyCode.A) && useTorque)
        {
            rb.angularVelocity = new Vector3(0, -rotationSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D) && useTorque)
        {
            rb.angularVelocity = new Vector3(0, rotationSpeed, 0);
        }
    }

    #endregion
}
