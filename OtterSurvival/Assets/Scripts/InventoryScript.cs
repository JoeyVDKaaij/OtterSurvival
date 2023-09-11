using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    private List<ItemObject> itemsInInventory = new List<ItemObject>();
    [SerializeField]
    private float maxGrabDistance = 1.1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hitInfo;

        if (Physics.Raycast(origin, direction, out hitInfo, maxGrabDistance))
        {
            GameObject hitItem = hitInfo.collider.gameObject;
            Debug.Log("pickup range");
            if (hitItem.CompareTag("Object"))
            {
                ObtainKey(hitItem);
            }
            if (hitItem.CompareTag("Gate"))
            {
                OpenGate(hitItem);
            }
        }

        if (itemsInInventory.Count > 0)
            Debug.Log(itemsInInventory[0]);
    }

    private void ObtainKey(GameObject key)
    {
        if (Input.GetKey(KeyCode.F))
        {
            itemsInInventory.Add(key.GetComponent<ItemScript>().item);
            Destroy(key);
        }
    }

    private void OpenGate(GameObject gate)
    {
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
}