using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float followSpeed = 6;
    public Transform player;

    void FixedUpdate() {
        updateMovement();
    }
    void updateMovement() {
        Vector3 newPos = new Vector3(player.position.x, player.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
