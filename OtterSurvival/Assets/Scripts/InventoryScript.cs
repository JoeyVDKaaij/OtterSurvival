using System.Collections.Generic;
using System.Linq;
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

        Debug.DrawLine(origin, direction * 10000, Color.magenta);

        if (Physics.Raycast(origin, direction, out hitInfo, maxGrabDistance))
        {
            Debug.Log("Hit");
            GameObject hitItem = hitInfo.collider.gameObject;
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

    private void ObtainKey(GameObject key)
    {
        if (Input.GetKey(KeyCode.F))
        {
            itemsInInventory.Append(key.GetComponent<ItemScript>().item);
            Destroy(key);
        }
    }

    private void OpenGate(GameObject gate)
    {
        if (itemsInInventory.Count > 0)
        {
            int[] keysOwned = { };
            for (int i = 0; i < itemsInInventory.Count; i++)
            {
                if (itemsInInventory[i].type == ItemObject.ItemType.Key)
                {
                    keysOwned.Append(i);
                }
            }
            if (keysOwned.Length > 0 && Input.GetKey(KeyCode.F))
            {
                Destroy(gate);
                itemsInInventory.Remove(itemsInInventory[keysOwned.Last()]);
            }
        }
    }
}
