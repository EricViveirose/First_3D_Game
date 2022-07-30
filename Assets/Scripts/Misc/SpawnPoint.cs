using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] collectiblePrefabArray;

    void Start()
    {
        try
        {
            int randomIndex = UnityEngine.Random.Range(0, collectiblePrefabArray.Length);

            Instantiate(collectiblePrefabArray[randomIndex], transform.position, transform.rotation);
            throw new UnassignedReferenceException("Keys not spawning. " + name + " cannot play");
        }

        finally

        {
            Debug.Log("Keys are in place. You may proceed");
        }

    }





}