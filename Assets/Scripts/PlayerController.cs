using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask treeLayer;
    public LayerMask depositeLayer;
    public InventoryManager inventoryManager;

    Vector2 playerMovement;
    float PlayerSize = 5f;
    float harvestRange = 1f;
    int fruitInventory = 0;
    int harvestRate = 1;
    int toolEndurance = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Speed", playerMovement.magnitude);
        if (Input.GetKeyDown(KeyCode.Space)) {
            Harvest();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            Deposite();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + playerMovement * moveSpeed * Time.fixedDeltaTime);
        if (playerMovement.x < 0)
        {
            Flip(false);
        }
        if (playerMovement.x > 0)
        {
            Flip(true);
        }
    }

    void Flip(bool bLeft) {
        transform.localScale = new Vector3(bLeft ? PlayerSize : -PlayerSize, PlayerSize, PlayerSize);
    }

    void Harvest() {
        Collider2D[] reachableTrees = Physics2D.OverlapCircleAll(transform.position, harvestRange, treeLayer);
        foreach (Collider2D tree in reachableTrees) {
            toolEndurance -= 1;
            fruitInventory += tree.gameObject.GetComponent<TreeController>().pickFruit(harvestRate);
            if (toolEndurance <= 0) {
                toolEndurance = 0;
                harvestRate = 1;
            }
        }
        Debug.Log("harvest " + fruitInventory + " fruits");
    }

    void Deposite() {
        Collider2D[] deposites = Physics2D.OverlapCircleAll(transform.position, harvestRange, depositeLayer);
        if (deposites.Length > 0) {
            Debug.Log("deposite " + fruitInventory + " fruits");
            fruitInventory = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Hoe")) {
            harvestRate = 2;
            toolEndurance = 2;
            bool result = inventoryManager.PickupItem(1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Apple")) {
            bool result = inventoryManager.PickupItem(0);
            Destroy(collision.gameObject);
        }
    }

    //void ondrawgizmosselected()
    //{
    //    gizmos.drawwiresphere(transform.position, harvestrange);
    //}
}
