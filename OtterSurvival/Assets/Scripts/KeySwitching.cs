using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeySwitching : MonoBehaviour
{
    [SerializeField, Tooltip("Put all the keysprites in here in order from first to last")]
    private Sprite[] sprites;
    private int spriteId = 0;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        if (sprites != null)
        {
            image.sprite = sprites[spriteId];
        }
    }

    // Switch to the next sprite
    public void NextSprite()
    {
        if (sprites != null)
        {
            spriteId += 1;
            image.sprite = sprites[spriteId];
        }
    }

    // Switch to the previous sprite
    public void PreviousSprite()
    {
        spriteId -= 1;
        image.sprite = sprites[spriteId];

    }

    // Switch to the first sprite
    public void FirstSprite()
    {
        spriteId += 1;
        image.sprite = sprites[0];

    }

    // Switch to the last sprite
    public void LastSprite()
    {
        spriteId += 1;
        image.sprite = sprites[sprites.Length-1];
    }
}
