using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemObject : ScriptableObject
{
    public enum ItemType
    {
        Null,
        RedKey,
        RustyKey,
        KeyBlackout,
        KeyOrange,
        Scissors,
        KeyBase,
        Wrench,
        KeyYellow,
        Axe
    }

    public ItemType type;
}
