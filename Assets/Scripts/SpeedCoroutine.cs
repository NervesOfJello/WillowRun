using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCoroutine : MonoBehaviour {

    private int seconds;
    [SerializeField]
    private float LevelAcceleration;

    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;
    
    public Rigidbody2D rb2; 
	// Use this for initialization
	void Start () {
        rb2 = GetComponent<Rigidbody2D>();

        StartCoroutine(RunTimer());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private IEnumerator RunTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;

            if (LevelAcceleration * seconds < minSpeed)
                rb2.velocity = new Vector2(minSpeed, 0);

            else if (LevelAcceleration * seconds > maxSpeed)
                rb2.velocity = new Vector2(maxSpeed, 0);

            else
                rb2.velocity = new Vector2((LevelAcceleration * seconds), 0);
        }
    }
}
