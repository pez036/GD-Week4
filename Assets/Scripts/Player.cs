using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    
    private void Start() {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        
        ItemWorld.SpawnItemWorld(new Vector3(2, 0), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(-2, 0), new Item { itemType = Item.ItemType.Apple, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 0), new Item { itemType = Item.ItemType.DashPotion, amount = 1 });
    }
    
    private void OnTriggerEnter2D(Collider2D collider) {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null) {
            // touching item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
}
