using UnityEngine;

public class TrashController : MonoBehaviour
{ 
    [SerializeField, Tooltip("Set how much extra air gets decreased")]
    private float addedDecreaseAirTime = 10f;

    [Header("Audio")]
    [SerializeField, Tooltip("The source where trash will be played from")]
    private AudioSource trashSource = null;
    [SerializeField, Tooltip("The clip that plays the sound effect")]
    private AudioClip trashClip = null;
    [SerializeField, Tooltip("The volume of the sound effect"), Range(0,1)]
    private float trashVolume = 1f;

    // Decrease more air given by the number above or in the inspector
    private void OnTriggerStay(Collider other)
    {
        if (trashSource != null && trashClip != null)
        {
            AudioManager.Instance.PlaySoundEffectWhenSilent(trashSource, trashVolume, trashClip);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<AirController>().AddedDecreaseAirSpeed(addedDecreaseAirTime);
        }
    }
}
