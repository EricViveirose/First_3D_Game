//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CheckForPlayer : MonoBehaviour
//{

//    public Transform playerTransform;
//    public float sightDistance = 200f;
//    public Transform originPoint;
//    public float speed;
//    public float rotationSpeed;
//    private Vector3 playerPosition;


//    void Update()
//    {
//        if (!playerTransform)                                                                //Can't see player, rotate
//            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

//        else if (speed < 0)
//            transform.rotation = playerTransform.rotation;

//        else                                                                                 //if he does see player, look at player
//            transform.LookAt(playerPosition);

//        RaycastHit hit;
//        if (Physics.Raycast(originPoint.position, transform.forward, out hit, sightDistance))  //origin, direction travelling, output/what to hit, distance projectile travels) if in line of sight, hit player 
//        {
//            if (hit.transform.gameObject.tag == "Player")
//            {
//                playerTransform = hit.transform;
//            }
//        }

//        Vector3 dir = transform.TransformDirection(Vector3.forward) * sightDistance;
//        Debug.DrawRay(originPoint.position, dir, Color.red);

//        if (playerTransform)                                                                        //Can see player
//        {
//            playerPosition = new Vector3(playerTransform.position.x, 30f, playerTransform.position.z);    
//            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed + 0.15f);
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public float sightDistance = 100f;
    public Transform originPoint;
    public float speed;
    public float rotationSpeed;
    private Vector3 playerPosition;

    // Update is called once per frame
    void Update()
    {
        if (!playerTransform)
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        else if (speed < 0)
            transform.rotation = playerTransform.rotation;
        else
            transform.LookAt(playerPosition);

        RaycastHit hit;
        if (Physics.Raycast(originPoint.position, transform.forward, out hit, sightDistance))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                playerTransform = hit.transform;

            }
        }

        Vector3 dir = transform.TransformDirection(Vector3.forward) * sightDistance;
        Debug.DrawRay(originPoint.position, dir, Color.red);

        if (playerTransform)
        {
            playerPosition = new Vector3(playerTransform.position.x, 30f, playerTransform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed);
        }
    }
}
