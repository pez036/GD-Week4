using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        Apple,
        DashPotion,
        Axe,
        Sword,
    }
    
    public ItemType itemType;
    public int amount;
}
