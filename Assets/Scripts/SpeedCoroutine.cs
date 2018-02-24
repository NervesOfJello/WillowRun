using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCoroutine : MonoBehaviour {
    private int seconds;
    public float vectorFloatX;
    public float vectorFloatY;
    
    public Rigidbody2D rb2; 
	// Use this for initialization
	void Start () {
        rb2 = GetComponent<Rigidbody2D>();

        StartCoroutine(RunTimer());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator RunTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;

            rb2.velocity = new Vector2((vectorFloatX * seconds), vectorFloatY);
            
        }
    }
}
