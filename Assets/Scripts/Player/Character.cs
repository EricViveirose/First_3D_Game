using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{

    CharacterController controller;

    [Header("Player Settings")]
    [Space(10)]
    [Tooltip("Speed value between 1 and 10")]
    [Range(1.0f, 10.0f)]
    public float speed = 10;
    public float gravity = 9.81f;
    public float jumpSpeed = 10.0f;

    Vector3 moveDir;

    [Header("Weapon Settings")]
    [Space(10)]
    public float projectileForce = 10.0f;
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPointLeft;
    public Transform projectileSpawnPointRight;



    void Start()
    {
        controller = GetComponent<CharacterController>();

        controller.minMoveDistance = 0.0f;

        if (speed <= 0.0f)
        {
            speed = 10.0f;
        }

        if (jumpSpeed <= 0.0f)
        {
            jumpSpeed = 10.0f;
        }
    }


    void Update()
    {
        try
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (controller.isGrounded)
            {
                moveDir = new Vector3(horizontal, 0.0f, vertical);
                moveDir *= speed;

                moveDir = transform.TransformDirection(moveDir);

                if (Input.GetButtonDown("Jump"))
                {
                    moveDir.y = jumpSpeed;
                }

            }
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                FireLeft();
                throw new UnassignedReferenceException("Fire1 not assigned " + name + " revert back to default " + Input.GetButtonDown("Fire1"));
            }

            if (Input.GetButtonDown("Fire2"))
                FireRight();
        }
        finally
        {
            Debug.Log("You may shoot. pew pew");
        }
    }

    void FireLeft()
    {
        if(projectilePrefab && projectileSpawnPointLeft)
        {
            Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPointLeft.position, projectileSpawnPointLeft.rotation);

            temp.AddForce(projectileSpawnPointLeft.forward * projectileForce, ForceMode.Impulse);

            Destroy(temp.gameObject, 2.0f);
        }
    }

    void FireRight()
    {
        if (projectilePrefab && projectileSpawnPointRight)
        {
            Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPointRight.position, projectileSpawnPointRight.rotation);

            temp.AddForce(projectileSpawnPointRight.forward * projectileForce, ForceMode.Impulse);

            Destroy(temp.gameObject, 2.0f);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Collectible")
        {
            Destroy(hit.gameObject);
        }
    }
}
