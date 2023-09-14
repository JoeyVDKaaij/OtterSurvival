using UnityEngine;
using UnityEngine.Playables;

public class PipeScript : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector pipeCloseAnimation;
    [SerializeField]
    private GameObject[] removeableSludge;

    [HideInInspector]
    public bool conditionsMet;
    private bool firstPlay;

    void Update()
    {
        // Checks if conditions are met
        if (conditionsMet && pipeCloseAnimation != null) PipeClose();
    }

    private void PipeClose()
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
}