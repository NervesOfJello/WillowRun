using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour {

    //Note: this scipt moves whatever it's attached to
    [SerializeField]
    private float BaseSpeed = 10;

    private float Speed;
    private float XInput;
    private float YInput;
    private Rigidbody2D rigidbody2D;
    
    private float waterMeterLevel;
    [SerializeField]
    private float waterRecoverRate = 0.5f;
    [SerializeField]
    private float mudSlowDown = 0.75f;

    private bool isInMud = false;

    [SerializeField]
    private float maxWaterMeter = 100;

    [SerializeField]
    private string HorizontalInputAxis;
    [SerializeField]
    private string VerticalInputAxis;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Speed = BaseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        //TODO get input from controller
        XInput = Input.GetAxis(HorizontalInputAxis);
        YInput = Input.GetAxis(VerticalInputAxis);
    }

    private void Move()
    {
        //TODO move object
        rigidbody2D.velocity = new Vector2(XInput * Speed, YInput * Speed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger");

        if (collision.tag == "Water")
        {
            if (waterMeterLevel < maxWaterMeter)
            {
                waterMeterLevel += waterRecoverRate;
            }

            Debug.Log("Enter Water");
        }
    }

    
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
            Speed = BaseSpeed;
            isInMud = false;
            Debug.Log("Exit Mud");
        }
    }
}
