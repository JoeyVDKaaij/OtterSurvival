using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class PipeScript : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] [Tooltip("Apply the animation of a pipe closing. Leaving it empty will skip the animation entirely")]
    private Animator pipeCloseAnimation;
    [Header("Sludge")]
    [SerializeField] [Tooltip("Remove the objects in this array once the pipe closes")]
    private GameObject[] removeableSludge;
    [Header("Enemy")]
    [SerializeField] [Tooltip("Spawn the enemies at the gameObject location(s)")]
    private GameObject[] enemySpawner = null;
    [Header("Oil")]
    [SerializeField] [Tooltip("Spawn the oil at the gameObject location(s)")]
    private GameObject[] oilSpawner = null;
    [SerializeField] [Tooltip("Spawn the big oil at the gameObject location(s)")]
    private GameObject[] bigOilSpawner = null;

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
           
                pipeCloseAnimation.SetBool("Close", true);
            

            // Removes object(s) after pipe closing has finished playing 
            if (pipeCloseAnimation.GetBool("Close"))
            {
                if (removeableSludge != null)
                {
                    foreach (GameObject removeableObject in removeableSludge) Destroy(removeableObject);
                }

                if (enemySpawner != null)
                {
                    foreach (GameObject spawnableObject in enemySpawner)
                        spawnableObject.SetActive(true);

                }
                if (oilSpawner != null)
                {
                    foreach (GameObject spawnableObject in oilSpawner)
                    {
                        spawnableObject.SetActive(true);
                    }
                }
                if (bigOilSpawner != null)
                {
                    foreach (GameObject spawnableObject in bigOilSpawner)
                    {
                        spawnableObject.SetActive(true);
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
                    spawnableObject.SetActive(true);
                }
            }
            if (oilSpawner != null)
            {
                foreach (GameObject spawnableObject in oilSpawner)
                {
                    spawnableObject.SetActive(true);
                }
            }
            if (bigOilSpawner != null)
            {
                foreach (GameObject spawnableObject in bigOilSpawner)
                {
                    spawnableObject.SetActive(true);
                }
            }
        }
    }
}