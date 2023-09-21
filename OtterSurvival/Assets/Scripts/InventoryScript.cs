using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryScript : MonoBehaviour
{
    private enum mouseButton
    {
        left,
        right, 
        middle
    }


    private List<ItemObject> itemsInInventory = new List<ItemObject>();
    [Header("Item Oriented Variables")]
    [SerializeField, Tooltip("The distance that the player can grab from")]
    private float maxGrabDistance = 1.1f;
    [SerializeField, Tooltip("All the available items")]
    private List<ItemObject> availableItems = new List<ItemObject>();
    [SerializeField, Tooltip("The Keyswitching script")]
    private KeySwitching keySwitching = null;
    [SerializeField, Tooltip("The Keyswitching script")]
    private GameObject axe = null;
    [SerializeField, Tooltip("The Keyswitching script")]
    private GameObject scissors = null;
    [SerializeField, Tooltip("The Keyswitching script")]
    private GameObject wrench = null;


    [Header("Sprite and Animations")]
    [SerializeField, Tooltip("The sprite of bubbles")]
    private GameObject Bubbles;
    [SerializeField, Tooltip("Animation of bubbles")]
    private Animator bubblesAnimator;

    [Header("Interactions")]
    [SerializeField, Tooltip("The button for interacting with the item")]
    private KeyCode interaction = KeyCode.F;
    [SerializeField, Tooltip("The mouse button for interacting with the item")]
    private mouseButton interactionMouse = mouseButton.left;
    private int mouseButtonN;

    [Header("Audio")]
    private List<AudioClip> itemSoundEffects = new List<AudioClip>();
    private List<float> itemVolumes = new List<float>();
    [SerializeField, Tooltip("Sound effect clip for using an item")]
    private AudioClip pickUp = null;
    [SerializeField, Tooltip("The volume of the pick up openGate from 0 to 1"), Range(0f, 1f)]
    private float pickUpVolume = 1f;
    [SerializeField, Tooltip("Sound effect clip for using an item")]
    private AudioClip openGate = null;
    [SerializeField, Tooltip("The volume of the pick up openGate from 0 to 1"), Range(0f, 1f)]
    private float openGateVolume = 1f;
    [SerializeField, Tooltip("Sound effect clip for using an item")]
    private AudioClip openCage = null;
    [SerializeField, Tooltip("The volume of the pick up open cage from 0 to 1"), Range(0f, 1f)]
    private float openCageVolume = 1f;

    private void Start()
    {
        switch (interactionMouse)
        {
            case mouseButton.left:
                mouseButtonN = 0; break;

            case mouseButton.right:
                mouseButtonN = 1; break;

            case mouseButton.middle:
                mouseButtonN = 2; break;
        }
    }

    #region RayCast and Hits

    void Update()
    {
        // Make a raycast and see if it hits anything
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hitInfo;

        if (Physics.Raycast(origin, direction, out hitInfo, maxGrabDistance))
        {
            GameObject hitItem = hitInfo.collider.gameObject;

            // Activate function depending on which tag the hit object is
            if (hitItem.CompareTag("Object"))
            {
                ObtainKey(hitItem);
            }
            if (hitItem.CompareTag("Gate"))
            {
                OpenGate(hitItem);
            }
            if (hitItem.CompareTag("Cage"))
            {
                OpenCage(hitItem);
            }
        }
    }


    #endregion

    #region Object Functions

    private void ObtainKey(GameObject key)
    {
        if (Input.GetKey(KeyCode.F))
        {
            // Add key to list and remove key object ingame
            itemsInInventory.Add(key.GetComponent<ItemScript>().item);
            bubblesAnimator.SetBool("IsInteracting", true);
            itemSoundEffects.Add(key.GetComponent<ItemScript>().itemSoundEffect);
            itemVolumes.Add(key.GetComponent<ItemScript>().itemVolume);
            if (pickUp != null)
                AudioManager.Instance.PlaySoundEffect(gameObject, pickUp, pickUpVolume);
            if (key.GetComponent<ItemScript>().item != availableItems[0] && key.GetComponent<ItemScript>().item != availableItems[7] && key.GetComponent<ItemScript>().item != availableItems[8] && keySwitching != null)
            {
                keySwitching.NextSprite();
            }
            else if (key.GetComponent<ItemScript>().item == availableItems[0] && axe != null)
            {
                axe.SetActive(true);
            }
            else if (key.GetComponent<ItemScript>().item == availableItems[8] && wrench != null)
            {
                wrench.SetActive(true);
            }
            else if (key.GetComponent<ItemScript>().item == availableItems[7] && scissors != null)
            {
                scissors.SetActive(true);
            }

            Destroy(key);
        }
    }

    private void OpenGate(GameObject gate)
    {
        // Checks if you have a key
        // If you have key remove the latest key from list and remove the hit gate object when pressing F
        if (itemsInInventory.Count > 0)
        {
            bool itemUsed = false;
            int itemId = 0;
            foreach (ItemObject item in itemsInInventory)
            {
                if (item == gate.GetComponent<ItemScript>().item && !itemUsed && Input.GetKey(interaction))
                {
                    if (gate.GetComponent<ItemScript>().item == availableItems[6] && keySwitching != null)
                    {
                       keySwitching.NextSprite();
                    }

                    gate.GetComponent<ItemScript>().ParticlePLay();

                    Destroy(gate);
                    itemUsed = true;
                    if (openGate != null && itemSoundEffects[itemId] == null)
                        AudioManager.Instance.PlaySoundEffect(gameObject, openGate, openGateVolume);
                    else
                        AudioManager.Instance.PlaySoundEffect(gameObject, itemSoundEffects[itemId], itemVolumes[itemId]);
                }

                itemId++;
            }
        }
    }

    private void OpenCage(GameObject cage) 
    {
        bool itemUsed = false;
        int itemId = 0;
        foreach (ItemObject item in itemsInInventory)
        {
            // Checks if you have the right key and open the gate
            if (item == cage.GetComponent<OtterCageScript>().item && !itemUsed && Input.GetKey(interaction) || item == cage.GetComponent<OtterCageScript>().item && !itemUsed && Input.GetMouseButton(mouseButtonN))
            {
                cage.GetComponent<OtterCageScript>().GateOpen();
                bubblesAnimator.SetBool("IsInteracting", true);
                itemUsed = true;
                if (openCage != null && itemSoundEffects[itemId] == null)
                    AudioManager.Instance.PlaySoundEffect(gameObject, openCage, openCageVolume);
                else
                    AudioManager.Instance.PlaySoundEffect(gameObject, itemSoundEffects[itemId], itemVolumes[itemId]);
            
                itemId++;
            }
        }
    }

    #endregion

}