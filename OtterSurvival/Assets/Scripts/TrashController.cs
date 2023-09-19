using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField, Tooltip("Set how much extra air gets decreased")]
    private float addedDecreaseAirTime = 10f;

    // Decrease more air given by the number above or in the inspector
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) other.gameObject.GetComponent<AirController>().AddedDecreaseAirSpeed(addedDecreaseAirTime);
    }
}
