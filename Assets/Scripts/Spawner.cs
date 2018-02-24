using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private float spawnDist = 15;
    public GameObject [] obstacles;
    private GameObject gameObject;


    private Vector3 cameraTopRight;
    private Vector3 cameraBottomRight;
    private System.Random rand = new System.Random();
    private float cameraLastPosition;
    private bool isSpawned;


    private void Start()
    {
        cameraLastPosition = Camera.main.transform.position.x;
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateCameraPosition();
        CheckSpawnDistance();

    }

    //Checking if the Main Camera has moved atleast spawnDist before spawning 3 more objects
    private void CheckSpawnDistance()
    {
        if (Camera.main.transform.position.x - cameraLastPosition >= spawnDist)
        {
            SpawnObject();
            SpawnObject();
            SpawnObject();
            isSpawned = true;
        }
    }

    private void SpawnObject()
    {
        int num = rand.Next(0, 100);
        int cameraEdgeX = rand.Next((int)cameraTopRight.x, (int)cameraBottomRight.x);
        int cameraEdgeY = rand.Next((int)cameraBottomRight.y, (int)cameraTopRight.y);
        Vector3 pos = new Vector3(cameraEdgeX, cameraEdgeY);

        if (num <= 60)
        {
            gameObject = Instantiate(obstacles[0], pos, Quaternion.identity, this.transform);
        }
        else
        {
            gameObject = Instantiate(obstacles[1], pos, Quaternion.identity, this.transform);
        }

        //Destroys object 8 seconds after it spawns
        Destroy(gameObject, 8);
    }

    private void UpdateCameraPosition()
    {
        cameraTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        cameraBottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0));

        if (isSpawned)
        {
            //If it this script has spawned then update the new cameraLastPosition and Set isSpawned back to false
            cameraLastPosition = Camera.main.transform.position.x;
            isSpawned = false;
        }
        
    }
}
