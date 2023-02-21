using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectedCount : MonoBehaviour
{

    public TextMeshProUGUI countText;
    int count = 0;
    // Start is called before the first frame update
    void Start() {
        countText.text = "x " + count.ToString();
    }

    public void UpdateCount(int countToAdd) {
        count += countToAdd;
        countText.text = "x " + count.ToString();
    }
}
