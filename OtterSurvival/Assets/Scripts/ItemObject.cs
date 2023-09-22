using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

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

    [Tooltip("Set the item")]
    public ItemType type;

    
  }

