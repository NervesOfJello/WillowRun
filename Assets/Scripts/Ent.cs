using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour {

    //Note: this scipt moves whatever it's attached to
    //movement variables/inputs
    [SerializeField]
    private float BaseSpeed = 10;
    [SerializeField]
    private float BoostRatio;

    private float Speed;
    private float SpeedRatio = 1;
    private float BaseSpeedRatio = 1;
    private float XInput;
    private float YInput;
    private float BoostInput;

    private Rigidbody2D rigidbody2D;
    
    //water and mud variables
    private float waterMeterLevel;
    [SerializeField]
    private float waterRecoverRate = 0.5f;
    [SerializeField]
    private float mudSlowDown = 0.75f;

    private bool isInMud = false;

    [SerializeField]
    private float maxWaterMeter = 100;

    //input strings
    [SerializeField]
    private string HorizontalInputAxis;
    [SerializeField]
    private string VerticalInputAxis;
    [SerializeField]
    private string BoostInputAxis;

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
        Boost();
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
        BoostInput = Input.GetAxis(BoostInputAxis);
        Debug.Log("Boost Input: " + BoostInput);
    }

    private void Move()
    {
        //TODO move object
        Speed *= SpeedRatio;
        rigidbody2D.velocity = new Vector2(XInput * Speed, YInput * Speed);
    }

    private void Boost()
    {
        
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
