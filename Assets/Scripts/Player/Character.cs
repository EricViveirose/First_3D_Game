using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class Character : MonoBehaviour
{
    CharacterController controller;
    Animator anim;

    [Header("Player Settings")]
    [Space(10)]
    [Tooltip("Speed value between 1 and 20")]
    [Range(1.0f, 20.0f)]
    public float speed = 20;
    public float gravity = 9.81f;
    public float jumpSpeed = 10.0f;

    Vector3 moveDir;

    [Header("Weapon Settings")]
    [Space(10)]
    public float projectileForce = 10.0f;
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        controller.minMoveDistance = 0.0f;

        if (speed <= 0.0f)
        {
            speed = 20.0f;

        }

        if (jumpSpeed <= 0)
        {
            jumpSpeed = 10.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        try
        {

            if (controller.isGrounded)

            {
                moveDir = new Vector3(horizontal, 0.0f, vertical);
                moveDir *= speed;

                moveDir = transform.TransformDirection(moveDir);

                if (Input.GetButtonDown("Jump"))
                {
                    moveDir.y = jumpSpeed;
                    anim.SetTrigger("Jump");
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    anim.SetTrigger("Punch");
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetTrigger("Kick");
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("Shoot");
                }
            }

            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);

            anim.SetFloat("horizontal", horizontal);
            anim.SetFloat("vertical", vertical);

            if (Input.GetButtonDown("Fire1"))
                Fire1();
            throw new UnassignedReferenceException("Shooting Disabled " + name + " Shooting Enabled " + Input.GetButtonDown("Fire1"));
        }
        finally
        {
            Debug.Log("Shooting has been Enabled");
        }
    }

    void Fire1()
    {
        anim.SetTrigger("Shoot");
        if (projectilePrefab && projectileSpawnPoint)
        {
            Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            temp.AddForce(projectileSpawnPoint.forward * projectileForce, ForceMode.Impulse);

            Destroy(temp.gameObject, 5.0f);
        }
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Collectible")
        {
            Destroy(hit.gameObject);
        }

        if (hit.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("GameOver");
        }

        if (hit.gameObject.tag == "Liquid")
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}