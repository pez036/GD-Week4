using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class DeerController : MonoBehaviour
{
    public BoxCollider2D playerCollider;
    public TilemapCollider2D obstacles;
    Vector2 position;
    
    GameObject [] apples;
    GameObject appleToEat;
    
    BoxCollider2D thisCollider;
    BoxCollider2D appleCollider;
    float moveSpeed = 0.005f;
    float harvestRate = 5;


    void Start()
    {
        thisCollider = gameObject.GetComponent<BoxCollider2D>();
        apples = GameObject.FindGameObjectsWithTag("Tree");
        appleToEat = determineClosestApple();
        appleCollider = appleToEat.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(thisCollider, playerCollider);
        Physics2D.IgnoreCollision(thisCollider, obstacles);
    }
    
    void FixedUpdate() {
        //rb.MovePosition(rb.position + position * Time.fixedDeltaTime);
        if (position.x < 0) { Flip(false); }
        if (position.x > 0) { Flip(true); }
    }
    
    void Flip(bool bLeft) {
        transform.localScale = new Vector3(bLeft ? 5 : -5, 5, 5);
    }
    
    GameObject determineClosestApple() {
        GameObject closestApple = null;
        double closestAppleDist = double.PositiveInfinity;
        
        foreach (GameObject apple in apples) {
            if (apple == null) {
                continue;
            }
            
            double dist = Distance(gameObject.transform.position, apple.transform.position);
            if (dist < closestAppleDist) {
                closestAppleDist = dist;
                closestApple = apple;
                appleCollider = apple.GetComponent<BoxCollider2D>();
            }
        }
        return closestApple;
    }
    
    double Distance(Vector2 deerPos, Vector2 applePos) {
        // sqrt (x2-x1)^2 + (y2-y1)^2
        return Math.Abs(Math.Sqrt(Math.Pow((deerPos.x - applePos.x), 2) + Math.Pow((deerPos.y - applePos.y), 2)));
    }
    
    void Update()
    {
        // edit this to change movement
        //Vector2 newPos = new Vector2(position.x + 0.001f, position.y);
        Vector2 newPos = transform.position;
        if (appleToEat != null) {
            newPos = Vector2.MoveTowards(transform.position, appleToEat.transform.position, moveSpeed);
        }
        else {
            appleToEat = determineClosestApple();
            newPos = Vector2.MoveTowards(transform.position, appleToEat.transform.position, moveSpeed);
        }
        
        transform.position = newPos;
        
        if (appleToEat != null) {
            if (thisCollider.IsTouching(appleCollider))
            {
                // Do somet$$anonymous$$ng;
                //Debug.Log("HELLO");
                
                Destroy(appleToEat, appleToEat.GetComponent<TreeController>().fruitCount / harvestRate);
                
                appleToEat = determineClosestApple();
            }
        } else {
            appleToEat = determineClosestApple();
        }
    }
}
