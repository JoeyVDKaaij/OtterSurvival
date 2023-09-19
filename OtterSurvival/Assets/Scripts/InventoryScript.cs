using System.Collections.Generic;
using UnityEngine;

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
                bool itemUsed = false;
                foreach (ItemObject item in itemsInInventory)
                {
                    // Checks if you have the right key and open the gate
                    if (item == hitItem.GetComponent<OtterCageScript>().item && !itemUsed && Input.GetKey(interaction) || item == hitItem.GetComponent<OtterCageScript>().item && !itemUsed && Input.GetMouseButton(mouseButtonN))
                    {
                        hitItem.GetComponent<OtterCageScript>().GateOpen();
                        bubblesAnimator.SetBool("IsInteracting",true);
                        itemUsed = true;
                    }
                }
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
            bubblesAnimator.SetBool("IsInteracting",true);
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
            foreach (ItemObject item in itemsInInventory)
            {
                if (item == gate.GetComponent<ItemScript>().item && !itemUsed && Input.GetKey(interaction))
                {
                    Destroy(gate);
                    itemUsed = true;
                }
            }
        }
    }

    #endregion

}