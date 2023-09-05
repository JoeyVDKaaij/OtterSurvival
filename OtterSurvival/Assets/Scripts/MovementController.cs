using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

// This script requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float movementVelocityMultiplier = 5;
    [SerializeField]
    private float rotationTorqueSpeed = 10;
    public float damping = 5f;
    private bool underWater = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    #region Movements

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * movementVelocityMultiplier;
        }

    }

    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            rb.angularVelocity = new Vector3(0, -rotationTorqueSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.angularVelocity = new Vector3(0, rotationTorqueSpeed, 0);
        }
    }

    #endregion
}
