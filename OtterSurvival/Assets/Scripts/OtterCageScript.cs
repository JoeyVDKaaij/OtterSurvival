using UnityEngine;

public class OtterCageScript : MonoBehaviour
{
    [SerializeField, Tooltip("Set the pipe that gets affected by the unlocking of the cage")]
    private GameObject affectedPipe;

    [Tooltip("The item that is needed to open the cage")]
    public ItemObject item;

    public void GateOpen()
    {
        // Let the pipe Script know it can close and do it's closing effect
        affectedPipe.GetComponent<PipeScript>().conditionsMet = true;
        Destroy(gameObject);
    }
}
