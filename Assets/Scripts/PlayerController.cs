using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask treeLayer;
    public LayerMask depositeLayer;
    public InventoryManager inventoryManager;
    public CollectedCount collectedCount;
    public PlayerTool toolInHand;
    public AudioSource harvestSound;
    public AudioSource depositeSound;

    Vector2 playerMovement;
    float PlayerSize = 5f;
    float harvestRange = 1f;
    //int fruitInventory = 0;
    int harvestRate = 1;
    int toolEndurance = 0;
    int fruitCountGoal = 20;

    // Update is called once per frame
    void Update()
    {
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Speed", playerMovement.magnitude);
        if (Input.GetKeyDown(KeyCode.Space)) {
            anim.SetTrigger("SwingTool");
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            Deposite();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public void Harvest() {
        
        Collider2D[] reachableTrees = Physics2D.OverlapCircleAll(transform.position, harvestRange, treeLayer);
        if (reachableTrees.Length > 0) {
            harvestSound.Play();
        }
        foreach (Collider2D tree in reachableTrees) {
            toolEndurance -= 1;
            int fruitToPick = tree.gameObject.GetComponent<TreeController>().pickFruit(harvestRate);
            inventoryManager.GetFruit(fruitToPick);
            if (toolEndurance <= 0) {
                toolEndurance = 0;
                harvestRate = 1;
                inventoryManager.deleteTool();
                toolInHand.lostTool();
            }
        }
        //Debug.Log("harvest " + fruitInventory + " fruits");
    }

    void Deposite() {
        Collider2D[] deposites = Physics2D.OverlapCircleAll(transform.position, harvestRange, depositeLayer);
        if (deposites.Length > 0) {
            depositeSound.Play();
            int picked = inventoryManager.DepositeFruit();
            int currentPicked = collectedCount.UpdateCount(picked);
            if (currentPicked >= fruitCountGoal) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Shovel")) {
            Sprite toolSP = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            harvestRate = 2;
            toolEndurance = 2;
            inventoryManager.GetTool(2);
            toolInHand.setToolSprite(toolSP);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Fork")) {
            Sprite toolSP = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            harvestRate = 2;
            toolEndurance = 2;
            inventoryManager.GetTool(3);
            toolInHand.setToolSprite(toolSP);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Hoe")) {
            Sprite toolSP = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            harvestRate = 2;
            toolEndurance = 2;
            inventoryManager.GetTool(1);
            toolInHand.setToolSprite(toolSP);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Apple")) {
            inventoryManager.GetFruit(1);
            Destroy(collision.gameObject);
        }
    }

    //void ondrawgizmosselected()
    //{
    //    gizmos.drawwiresphere(transform.position, harvestrange);
    //}
}
