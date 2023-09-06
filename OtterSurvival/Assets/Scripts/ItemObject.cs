using UnityEngine;



[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemObject : ScriptableObject
{
    public enum ItemType
    {
        Null,  
        Key
    }

    public ItemType type;
}
