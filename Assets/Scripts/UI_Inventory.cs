using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    
    private void Awake() {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = transform.Find("itemSlotTemplate");
        Debug.Log(itemSlotContainer);
        Debug.Log(itemSlotTemplate);
    }
    
    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;
        RefreshInventoryItems();
    }
    
    private void RefreshInventoryItems() {
        float x = 0;
        float y = 0;
        float itemSlotCellSize = 85f;
        foreach (Item item in inventory.GetItemList()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x++;
            
            // TODO: change based on size of this inventory
            // TODO: limit inventory
            if (x > 4) {
                x = 0;
                y++;
            }
        }
    }
}
