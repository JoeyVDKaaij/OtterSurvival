using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AirController : MonoBehaviour
{
    [Header("Hud Elements")]
    [SerializeField, Tooltip("The slider")]
    private Slider slider;

    [Header("Air Variables")]
    [SerializeField, Tooltip("How long the player can breathe")]
    private float maxAirTime = 100;
    [SerializeField, Tooltip("How fast the amount of air is decreasing")]
    private float decreaseAirSpeed = 5;
    [SerializeField, Tooltip("How fast the amount of air is increasing")]
    private float increaseAirSpeed = 10;
    private float air;

    private PlayableDirector deathAnimation;
    private bool firstPlay = true;
    private Volume volume;
    private Vignette lowAir;

    void Start()
    {
        deathAnimation = GetComponent<PlayableDirector>();

        volume = GetComponentInChildren<Volume>();

        air = maxAirTime;

        if (slider != null)
            slider.maxValue = maxAirTime;
    }

    #region Breathing and air meter

    private void OnCollisionStay(Collision collision)
    {
        // Set breathing to true to get air back
        // This gets disabled again at the end of the script
        if (collision.collider.CompareTag("Sky"))
        {
            // Return decrease air and add increase air on top of it
            air += increaseAirSpeed; 
            air += decreaseAirSpeed;
        }
    }

    void FixedUpdate()
    {
        // Breathing mechanic
        air -= decreaseAirSpeed;

        // Change the slider to adapt to the amount of air
        if (slider != null)
            slider.value = air;

        // Game kills you if you don't have any air left
        if (air <= 0) Death();

        // Sets the intensity of the vignette
        if (volume != null)
        {
            volume.weight = 1f - (air / maxAirTime);
            volume.weight = Mathf.Clamp(volume.weight, 0, 1f);
        }

        // If Air goes over the limit return it to maximum value
        air = Mathf.Clamp(air, 0, maxAirTime);
    }

    public void AddedDecreaseAirSpeed(float pDecreaseAirSpeed)
    {
        air -= pDecreaseAirSpeed;
    }

    #endregion

    #region Game Over

    private void Death()
    {
        // Makes sure the player can't move
        gameObject.GetComponent<MovementController>().gameOver = true;
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;

        // Sees if the deathanimation is applied in the inspector
        // Otherwise go directly to the game over scene
        if (deathAnimation != null)
        {
            // Checks if this is the first time the death scene will be played
            if (firstPlay)
            {
                deathAnimation.Play();
                firstPlay = false;
            }

            // Once the death scene has been played at least once and it's not playing anymore. Go to game over scene
            if (deathAnimation.state != PlayState.Playing && !firstPlay)
            {
                // Death scene
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            // Death scene
            SceneManager.LoadScene(2);
        }
    }

    #endregion
}
