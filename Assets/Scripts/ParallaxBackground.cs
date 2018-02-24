using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    public float backgroundSize;

    private Transform cameraTransform;
    private Transform[] layers;

    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    private float lastCameraX;
    
	// Use this for initialization
	void Start () {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;
	}
	
	// Update is called once per frame
	void Update () {

       // lastCameraX = cameraTransform.position.x;

        if(cameraTransform.position.x > (layers[rightIndex].transform.position.x - backgroundSize))
        {
            ScrollRight();
        }
	}

    private void ScrollLeft()
    {
       // int lastRight = rightIndex;

        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    private void ScrollRight()
    {
        //int lastLeft = leftIndex;

        //Debug.Log("before move" + layers[leftIndex].position);
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        //Debug.Log("after move" + layers[leftIndex].position);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
