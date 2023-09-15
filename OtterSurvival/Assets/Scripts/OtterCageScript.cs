using UnityEngine;

public class OtterCageScript : MonoBehaviour
{
    [SerializeField]
    private GameObject affectedPipe;

    // Apply the object 
    public ItemObject item;

    public void GateOpen()
    {
        // Let the pipe Script know it can close and do it's closing effect
        affectedPipe.GetComponent<PipeScript>().conditionsMet = true;
        Destroy(gameObject);
    }
}
