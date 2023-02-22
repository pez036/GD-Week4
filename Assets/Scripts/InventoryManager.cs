using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItem = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Item[] items;
    public bool AddItem(Item item) {

        for (int i = 0; i < inventorySlots.Length; ++i) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItem && itemInSlot.item.stackable) {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; ++i) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    public bool PickupItem(int itemId) {
        if (itemId < items.Length) {
            return AddItem(items[itemId]);
        }
        return false;
    }

    public void GetFruit(int count) {
        for (int i = 0; i < count; ++i) {
            PickupItem(0);
        }
    }

    public void GetHoe() {
        for (int i = 0; i < inventorySlots.Length; ++i) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == items[1]) {
                return;
            }
        }
        PickupItem(1);
    }

    public int DepositeFruit() {
        int totalFruits = 0;
        for (int i = 0; i < inventorySlots.Length; ++i) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == items[0]) {
                totalFruits += itemInSlot.count;
                Destroy(itemInSlot.gameObject);
            }
        }
        return totalFruits;
    }

    public void deleteHoe() {
        for (int i = 0; i < inventorySlots.Length; ++i) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == items[1]) {
                Destroy(itemInSlot.gameObject);
                return;
            }
        }
    }

    void SpawnNewItem(Item item, InventorySlot slot) {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}
