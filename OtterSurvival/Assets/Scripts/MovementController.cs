using UnityEngine;

// This script requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    private float forwardAxis;
    private float rightAxis;
    private float upAxis;
    private float airSlowdownMultiplier;
    private float airSlowdownStandard = 1;

    [Header("Movements")]
    [SerializeField, Tooltip("How fast the player can move in units")]
    private float movementVelocityMultiplier = 5;

    [SerializeField, Tooltip("How much the player gets slown down")]
    private float airSlowdown = 0.5f;

    Vector2 rotation;
    Vector2 currentRotation;
    [Header("Rotations")]
    [SerializeField, Tooltip("Mouse Sensitivity")]
    private float sensitivity = 2.0f;
    [SerializeField, Tooltip("How far the rotation can reach in the Y axis")]
    private float minYRotation = -90f;
    [SerializeField, Tooltip("How far the rotation can reach in the Y axis")]
    private float maxYRotation = 90f;

    private Rigidbody rb;

    [HideInInspector]
    public bool gameOver = false;

    [Header("Sprite and Animations")]
    [SerializeField, Tooltip("The sprite of bubbles")]
    private GameObject Bubbles;
    [SerializeField, Tooltip ("Animation of bubbles")]
    private Animator bubblesAnimator;

    Billboard script;

    [Header("Audio")]
    [SerializeField, Tooltip("Sound effect source for picking something up")]
    private AudioSource movementAudioSource = null;
    [SerializeField, Tooltip("The volume of the pick up sound effect from 0 to 1"), Range(0f, 1f)]
    private float movementVolume = 1f;
    [SerializeField, Tooltip("Sound effect clip for the player diving into the water")]
    private AudioClip diveClip = null;
    [SerializeField, Tooltip("The volume of the player diving into the water sound effect from 0 to 1"), Range(0f, 1f)]
    private float diveVolume = 1f;
    [SerializeField, Tooltip("Sound effect clip for the player getting above water")]
    private AudioClip aboveWaterClip = null;
    [SerializeField, Tooltip("The volume of the player getting above water from 0 to 1"), Range(0f, 1f)]
    private float aboveWaterVolume = 1f;

    // Initiate variables
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        airSlowdownMultiplier = airSlowdownStandard;
        // Comment this out if you want to move your mouse anywhere freely during play mode
        Cursor.lockState = CursorLockMode.Locked;

        script = Bubbles.GetComponent<Billboard>();
        script.enabled = false;
    }

    #region Movements

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sky"))
        {
            AudioManager.Instance.PlaySoundEffect(gameObject, aboveWaterClip, aboveWaterVolume);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Set movement speed above water vs under water
        if (collision.collider.CompareTag("Sky"))
        {
            airSlowdownMultiplier = airSlowdown; 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Sky"))
        {
            airSlowdownMultiplier = airSlowdownStandard;
            AudioManager.Instance.PlaySoundEffect(gameObject, diveClip, diveVolume);
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
            bubblesAnimator.SetFloat("Speed", forwardAxis);

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
            if (forwardAxis != 0 || rightAxis != 0 || upAxis != 0 && movementAudioSource != null) AudioManager.Instance.PlaySoundEffectWhenSilent(movementAudioSource, movementVolume); 
        }
        if (gameOver)
        {
            script.enabled = true;
        }
    }

    #endregion
}