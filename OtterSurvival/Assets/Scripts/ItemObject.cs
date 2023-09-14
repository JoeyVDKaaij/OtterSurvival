using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemObject : ScriptableObject
{
    public enum ItemType
    {
        Null,  
        Gate,
        Key
    }

    public ItemType type;
}
