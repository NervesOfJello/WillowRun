using UnityEngine;
using System.Collections;

public class SimpleInput : MonoBehaviour {


    //Not this scipt moves whatever it's attached to

    public Vector3 inputDirection;
    public float Speed;
    private float orginalSpeed = 10;

    public Vector3 keyDirection;

    public float waterMeter;
    public float waterRecover = 0.5f;
    public float mudSlowDown = 0.75f;
    private const float MAXWATERMETER = 100;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //input
        keyDirection.x = keyDirection.y = 0;  //reset key direction on every frame this will stop movement if no ket is pressed

        UpdateInputFromKeyboard();

        //Move Pacman
        this.transform.position += this.inputDirection * this.Speed * Time.deltaTime;
    }

    private void UpdateInputFromKeyboard()
    {
        //Keyboard Input
        //Note this input uses keys not axis you shouldn't mix keys and axis unless you are carefull
        if (Input.GetKey("right"))
        {
            keyDirection.x += 1;
        }
        if (Input.GetKey("left"))
        {
            keyDirection.x += -1;
        }

        if (Input.GetKey("up"))
        {
            keyDirection.y += 1;
        }
        if (Input.GetKey("down"))
        {
            keyDirection.y += -1;
        }

        inputDirection = keyDirection;
        inputDirection.Normalize();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger");

        if (collision.tag == "Water")
        {
            if (this.waterMeter < MAXWATERMETER)
            {
                this.waterMeter += waterRecover;
            }

            Debug.Log("Enter Water");
        }
    }

    bool isInMud = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInMud)
        {
            if (collision.tag == "Mud")
            {
                Speed *= mudSlowDown;
                Debug.Log("Enter Mud");
                isInMud = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mud")
        {
            this.Speed = this.orginalSpeed;
            isInMud = false;
            Debug.Log("Exit Mud");
        }
    }
}
