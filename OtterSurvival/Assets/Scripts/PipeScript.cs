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
    [Header("Oil")]
    [SerializeField]
    [Tooltip("The oil prefab that is going to spawn")]
    private GameObject oil = null;
    [SerializeField] [Tooltip("Spawn the oil at the gameObject location(s)")]
    private GameObject[] oilSpawner = null;
    [Tooltip("The big oil prefab that is going to spawn")]
    private GameObject bigOil = null;
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
                if (oilSpawner != null)
                {
                    foreach (GameObject spawnableObject in oilSpawner)
                    { 
                        if (oil != null) Instantiate(oil, spawnableObject.transform);

                        Destroy(spawnableObject);
                    }
                }
                if (bigOilSpawner != null)
                {
                    foreach (GameObject spawnableObject in bigOilSpawner)
                    {
                        if (bigOil != null) Instantiate(bigOil, spawnableObject.transform);

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
            if (oilSpawner != null)
            {
                foreach (GameObject spawnableObject in oilSpawner)
                {
                    if (oil != null) Instantiate(oil, spawnableObject.transform);

                    Destroy(spawnableObject);
                }
            }
            if (bigOilSpawner != null)
            {
                foreach (GameObject spawnableObject in bigOilSpawner)
                {
                    if (bigOil != null) Instantiate(bigOil, spawnableObject.transform);

                    Destroy(spawnableObject);
                }
            }
        }
    }
}