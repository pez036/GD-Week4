using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    int fruitCount;
    // Start is called before the first frame update
    void Start() {
        fruitCount = Random.Range(1, 10);
    }

    public int pickFruit(int harvestRate) {
        if (fruitCount > harvestRate) {
            fruitCount -= harvestRate;
            return harvestRate;
        }
        int picked = fruitCount;
        fruitCount = 0;
        gameObject.SetActive(false);
        return picked;
    }
}
