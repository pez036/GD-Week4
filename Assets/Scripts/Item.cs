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
    
    public Sprite GetSprite() {
        switch(itemType) {
        default:
        case ItemType.Apple:        return ItemAssets.Instance.appleSprite;
        case ItemType.DashPotion:   return ItemAssets.Instance.dashPotionSprite;
        case ItemType.Axe:          return ItemAssets.Instance.axeSprite;
        case ItemType.Sword:        return ItemAssets.Instance.swordSprite;
        }
    }
}
