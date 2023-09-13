using UnityEngine;

// This script requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    private float forwardAxis;
    private float rightAxis;
    private float upAxis;
    private float airSlowdownMultiplier;
    [SerializeField]
    private float movementVelocityMultiplier = 5;


    private float airSlowdownStandard = 1;
    [SerializeField]
    private float airSlowdown = 0.5f;

    [SerializeField]
    private Animator animator;

    Vector2 rotation;
    Vector2 currentRotation;
    [SerializeField]
    private float sensitivity = 2.0f;
    [SerializeField]
    private float minYRotation = -90f;
    [SerializeField]
    private float maxYRotation = 90f;

    private Rigidbody rb;

    public bool gameOver = false;

    // Initiate variables
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        airSlowdownMultiplier = airSlowdownStandard;
        // Comment this out if you want to move your mouse anywhere freely during play mode
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Movements

    private void OnCollisionStay(Collision collision)
    {
        // Set movement speed above water vs under water
        if (collision.collider.CompareTag("Sky"))
        {
            airSlowdownMultiplier = airSlowdown; 
            
        }
        else
        {
            airSlowdownMultiplier = airSlowdownStandard;
        }
    }

    void Update()
    {
        if (!gameOver)
        {
            // Check button press based on horizontal input or vertical imput (WASD or Arrow keys for example)
            forwardAxis = Input.GetAxis("Vertical");
            rightAxis = Input.GetAxis("Horizontal");
            upAxis = Input.GetAxis("Raise");

            // Rotations using mouse
            rotation.x = Input.GetAxis("Mouse X");
            rotation.y = -Input.GetAxis("Mouse Y");

            // Set animation
            animator.SetFloat("Speed", forwardAxis);

            // Apply sensitivity
            rotation *= sensitivity;

            // Set the current rotation
            currentRotation += rotation;

            // Set the Y rotation between the set min or max rotation degrees if it goes over the limit
            currentRotation.y = Mathf.Clamp(currentRotation.y, minYRotation, maxYRotation);

            // Apply the rotation variable to the actual rotation of the gameobject
            transform.rotation = Quaternion.Euler(new Vector3(currentRotation.y, currentRotation.x, 0));
        }
    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            // Movement using force
            rb.AddForce(transform.forward * movementVelocityMultiplier * airSlowdownMultiplier * forwardAxis);
            rb.AddForce(transform.right * movementVelocityMultiplier * airSlowdownMultiplier * rightAxis);
            rb.AddForce(transform.up * movementVelocityMultiplier * airSlowdownMultiplier * upAxis);
        }
    }

    #endregion
}