using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    
    private void Start() {
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
    }
    
    private void OnTriggerEnter2D(Collider2D collider) {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null) {
            // touching item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    
    private void UseItem(Item item) {
        switch (item.itemType) {
            case Item.ItemType.Apple:
                // drop
                break;
            case Item.ItemType.DashPotion:
                // increase player speed briefly
                inventory.RemoveItem(new Item { itemType = Item.ItemType.DashPotion, amount = 1});
                break;
            case Item.ItemType.Axe:
                // ability to pick apples
                break;
            case Item.ItemType.Sword:
                // kill animals
                break;
        }
    }
}
