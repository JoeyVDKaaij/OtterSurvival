using UnityEngine;

// This script requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    private float forwardAxis;
    private float rightAxis;
    private float upAxis;
    [SerializeField]
    private float movementVelocityMultiplier = 5;

    Vector2 rotation;
    Vector2 currentRotation;
    [SerializeField]
    private float sensitivity = 2.0f;
    [SerializeField]
    private float minYRotation = -90f;
    [SerializeField]
    private float maxYRotation = 90f;

    private Rigidbody rb;

    // Initiate variables
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Comment this out if you want to move your mouse anywhere freely during play mode
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Movements

    void Update()
    {
        // Check button press based on horizontal input or vertical imput (WASD or Arrow keys for example)
        forwardAxis = Input.GetAxis("Vertical");
        rightAxis = Input.GetAxis("Horizontal");
        upAxis = Input.GetAxis("Raise");

        // Rotations using mouse
        rotation.x = Input.GetAxis("Mouse X");
        rotation.y = -Input.GetAxis("Mouse Y");

        // Apply sensitivity
        rotation *= sensitivity;

        // Set the current rotation
        currentRotation += rotation;

        // Set the Y rotation between the set min or max rotation degrees if it goes over the limit
        currentRotation.y = Mathf.Clamp(currentRotation.y, minYRotation, maxYRotation);

        // Apply the rotation variable to the actual rotation of the gameobject
        transform.rotation = Quaternion.Euler(new Vector3(currentRotation.y, currentRotation.x, 0));
    }

    private void FixedUpdate()
    {
        // Movement using force
        rb.AddForce(transform.forward * movementVelocityMultiplier * forwardAxis);
        rb.AddForce(transform.right * movementVelocityMultiplier * rightAxis);
        rb.AddForce(transform.up * movementVelocityMultiplier * upAxis);
    }

    #endregion
}