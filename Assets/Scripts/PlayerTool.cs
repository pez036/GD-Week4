using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTool : MonoBehaviour
{
    public Sprite[] toolSprites;
    public SpriteRenderer activeSprite;
    public int toolId;
    void Start() {
        activeSprite = GetComponent<SpriteRenderer>();    
    }

    public void getTool(int id) {
        toolId = id;
        activeSprite.sprite = toolSprites[id];
    }

    public int lostTool() {
        activeSprite.sprite = null;
        int usedToolId = toolId;
        toolId = -1;
        return usedToolId;
    }
}
