using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField]
    private float decreaseAirSpeed = 10f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) collision.gameObject.GetComponent<AirController>().decreaseAirSpeed = decreaseAirSpeed;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) collision.gameObject.GetComponent<AirController>().decreaseAirSpeed = decreaseAirSpeed;
    }
}
