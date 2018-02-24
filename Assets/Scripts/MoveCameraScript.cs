using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour {


    public GameObject player;

    private Vector3 offset;
	// Use this for initialization
	void Start () {

        offset = transform.position - player.transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        transform.position = (Vector3)player.transform.position + offset;
        
	}
}
