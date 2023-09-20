using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class PipeScript : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] [Tooltip("Apply the animation of a pipe closing. Leaving it empty will skip the animation entirely")]
    private Animation pipeCloseAnimation;
    [Header("Sludge")]
    [SerializeField] [Tooltip("Remove the objects in this array once the pipe closes")]
    private GameObject[] removeableSludge;
    [Header("Enemy")]
    [SerializeField]
    [Tooltip("The Enemy prefab that is going to spawn")]
    private GameObject enemy = null;
    [SerializeField] [Tooltip("Spawn the enemies at the gameObject location(s)")]
    private GameObject[] enemySpawner = null;

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

                if (enemySpawner != null)
                {
                    foreach (GameObject spawnableObject in enemySpawner)
                    {
                        if (enemy != null) Instantiate(enemy, spawnableObject.transform);

                        Destroy(spawnableObject);
                    }
                }
            }
        }
        else
        {
            if (removeableSludge != null)
            {
                foreach (GameObject removeableObject in removeableSludge) Destroy(removeableObject);
            }

            if (enemySpawner != null)
            {
                foreach (GameObject spawnableObject in enemySpawner)
                {
                    if (enemy != null) Instantiate(enemy, spawnableObject.transform);

                    Destroy(spawnableObject);
                }
            }
        }
    }
}