using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEdit : MonoBehaviour
{
    [Header("Sprite and Animations")]
    [SerializeField, Tooltip("The sprite of bubbles")]
    private GameObject Bubbles;
    [SerializeField, Tooltip("Animation of bubbles")]
    private Animator bubblesAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void StopAnimation(string message)
    {
        if (message.Equals("InteracEnded"))
        {
            bubblesAnimator.SetBool("IsInteracting", false);
        }
    }
}
