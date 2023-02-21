using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
    
    public bool isStackable() {
        switch (itemType) {
            default:
            case ItemType.Apple:
            case ItemType.DashPotion:
                return true;
            case ItemType.Axe:
            case ItemType.Sword:
                return false;
        }
    }
}
