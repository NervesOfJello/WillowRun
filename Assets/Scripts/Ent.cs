using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour {

    //Note: this scipt moves whatever it's attached to
    //movement variables/inputs
    [SerializeField]
    private float BaseSpeed = 10;

    private float Speed;
    private float XInput;
    private float YInput;
    private float BoostInput;

    private Rigidbody2D rigidbody2D;
    
    //puddle and mud variables
    private float waterMeterLevel;

    public float WaterMeterLevelAsPercentage
    {
        get { return waterMeterLevel / maxWaterMeter; }
    }

    [SerializeField]
    private float waterRecoverRate = 0.5f;
    [SerializeField]
    private float mudSlowDown = 0.75f;

    private bool isInMud = false;

    [SerializeField]
    private float maxWaterMeter = 100;

    //boost variables
    [SerializeField]
    private float BoostCost = 10;
    [SerializeField]
    private float BoostAmount;
    [SerializeField]
    private float BoostCooldownInSeconds = 5;
    private bool isBoosting = false;

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

    private void Boost()
    {
        if (!isBoosting && waterMeterLevel >= BoostCost)
        {
            if (BoostInput > 0.9)
            {
                StartCoroutine(BoostCoroutine());
            }
        }
    }

    IEnumerator BoostCoroutine()
    {
            Speed += BoostAmount;
            waterMeterLevel -= BoostCost;
            yield return new WaitForSeconds(BoostCooldownInSeconds);
            Speed = BaseSpeed;
    }
}
