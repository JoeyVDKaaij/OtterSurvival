using UnityEngine;
using UnityEngine.UI;

public class AirController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float maxAirTime = 100;
    public float decreaseAirSpeed = 5;
    [SerializeField]
    private float increaseAirSpeed = 10;
    private float air;

    void Start()
    {
        air = maxAirTime;
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

            // If Air goes over the limit return it to maximum value
            air = Mathf.Clamp(air, 0, maxAirTime);
        }
    }

    void Update()
    {
        // Breathing mechanic
        air -= decreaseAirSpeed;

        // Change the slider to adapt to the amount of air
        slider.value = air;

        // Game kills you if you don't have any air left
        if (air <= 0)
        {
            // Implement death
            Debug.Log("You died");
        }
    }

    #endregion

}
