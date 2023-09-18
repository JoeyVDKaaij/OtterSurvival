using UnityEngine;
using UnityEngine.Playables;

public class PipeScript : MonoBehaviour
{
    [SerializeField] [Tooltip("Apply the animation of a pipe closing. Leaving it empty will skip the animation entirely")]
    private PlayableDirector pipeCloseAnimation;
    [SerializeField] [Tooltip("Remove the objects in this array once the pipe closes")]
    private GameObject[] removeableSludge;

    [HideInInspector]
    public bool conditionsMet;
    private bool firstPlay;

    void Update()
    {
        // Checks if conditions are met
        if (conditionsMet) PipeClose();
    }

    private void PipeClose()
    {
        if (pipeCloseAnimation != null)
        {
            // Checks if the pipe closing animation has been played
            if (firstPlay)
            {
                pipeCloseAnimation.Play();
                firstPlay = false;
            }

            // Removes object(s) after pipe closing has finished playing 
            if (pipeCloseAnimation.state != PlayState.Playing && !firstPlay)
            {
                if (removeableSludge != null)
                {
                    foreach (GameObject removeableObject in removeableSludge) Destroy(removeableObject);
                }
            }
        }
        else
        {
            if (removeableSludge != null)
            {
                foreach (GameObject removeableObject in removeableSludge) Destroy(removeableObject);
            }
        }
    }
}