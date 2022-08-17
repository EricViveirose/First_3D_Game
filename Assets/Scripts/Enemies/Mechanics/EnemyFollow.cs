using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 playerPosition;
    public float speed;
    public float rotationSpeed;
    public Animator eAnim;

    void Update()
    {
        if (!playerTransform)
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        else if (speed < 0)
            transform.rotation = playerTransform.rotation;
        else
            transform.LookAt(playerPosition);
        if (playerTransform)
        {
            playerPosition = new Vector3(playerTransform.position.x, 30.0f, playerTransform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed);
        }
    }
}