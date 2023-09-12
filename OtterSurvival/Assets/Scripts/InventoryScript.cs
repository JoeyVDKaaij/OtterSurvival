using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    private List<ItemObject> itemsInInventory = new List<ItemObject>();
    [SerializeField]
    private float maxGrabDistance = 1.1f;

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
            Destroy(key);
        }
    }

    private void OpenGate(GameObject gate)
    {
        // Checks if you have a key
        // If you have key remove the latest key from list and remove the hit gate object when pressing F
        if (itemsInInventory.Count > 0)
        {
            int keysOwned = -1;
            for (int i = 0; i < itemsInInventory.Count; i++)
            {
                if (itemsInInventory[i].type == ItemObject.ItemType.Key)
                {
                    keysOwned = i;
                }
            }
            if (keysOwned >= 0 && Input.GetKey(KeyCode.F))
            {
                Destroy(gate);
                itemsInInventory.Remove(itemsInInventory[keysOwned]);
            }
        }
    }

    #endregion

}