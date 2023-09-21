using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineScript : MonoBehaviour
{
    [SerializeField, Tooltip("Add the playable Director here to play on start")]
    private PlayableDirector timeline = null;
    [SerializeField, Tooltip("Add objects that needs to be disabled after animation is done")]
    private GameObject[] disableObjects = null;
    [SerializeField, Tooltip("Add objects that needs to be enabled after animation is done")]
    private GameObject[] enableObjects = null;

    // Start is called before the first frame update
    void Start()
    {
        if (timeline != null)
        {
            timeline.Play();
        }
    }

    private void Update()
    {
        if (timeline != null) 
        {
            if (timeline.state != PlayState.Playing && disableObjects != null)
            {
                foreach (GameObject gameobject in disableObjects) 
                {
                    gameobject.SetActive(false);
                }
                foreach (GameObject gameobject in enableObjects) 
                {
                    gameobject.SetActive(true);
                }
            }
        }
    }
}
