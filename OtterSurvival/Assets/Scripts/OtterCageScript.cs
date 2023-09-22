using UnityEngine;

public class OtterCageScript : MonoBehaviour
{
    [SerializeField, Tooltip("Set the pipe that gets affected by the unlocking of the cage")]
    private GameObject affectedPipe;

    [SerializeField, Tooltip("Set the otter that gets disabled by the unlocking of the cage")]
    private GameObject otter;

    [Tooltip("The item that is needed to open the cage")]
    public ItemObject item;

    [SerializeField, Tooltip("The particle that plays when you open the cage")]
    private ParticleSystem particles;

    public void GateOpen()
    {
        // Let the pipe Script know it can close and do it's closing effect
        if (particles != null)
            particles.Play();
        if (otter != null)
            otter.SetActive(false);
        affectedPipe.GetComponent<PipeScript>().conditionsMet = true;
        Destroy(gameObject);
    }
}
