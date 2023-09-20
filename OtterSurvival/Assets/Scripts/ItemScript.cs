using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [Tooltip("Set the item to the object")]
    public ItemObject item;
    [Tooltip("Set the audioclip when picking up this item")]
    public AudioClip itemSoundEffect = null;
    [Tooltip("Set the volume of the sound effect"), Range(0,1)]
    public float itemVolume = 1f;
}