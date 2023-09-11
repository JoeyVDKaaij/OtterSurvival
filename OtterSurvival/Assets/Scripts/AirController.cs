using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirController : MonoBehaviour
{
    [SerializeField]
    private float maxAirTime = 100;
    public float decreaseAirSpeed = 5;
    [SerializeField]
    private float increaseAirSpeed = 10;
    private float Air;
    private bool breathing = false;

    void Start()
    {
        Air = maxAirTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Set breathing to true to get air back
        // This gets disabled again at the end of the script
        if (collision.collider.CompareTag("Sky")) breathing = true;
    }

    void Update()
    {
        // Breathing mechanic
        if (!breathing) Air -= decreaseAirSpeed;
        else if (Air < maxAirTime) Air += increaseAirSpeed;
        else Air = maxAirTime;

        // Game kills you if you don't have any air left
        if (Air <= 0)
        {
            // Implement death
            Debug.Log("You died");
        }

        breathing = false;
    }
}
