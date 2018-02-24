using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Start, Playing, Dead }
public class Ent : MonoBehaviour
{

    private PlayerState _playerState;
    public PlayerState PlayerState
    {
        get { return _playerState; }
    }
    //Note: this scipt moves whatever it's attached to
    //movement variables/inputs
    [SerializeField]
    private float BaseSpeed = 10;

    private float Speed;
    private float XInput;
    private float YInput;
    private float BoostInput;
    private float WhipInput;

    private new Rigidbody2D rigidbody2D;
    
    //puddle and mud variables
    private float waterMeterLevel;
    //public field for the water meter bar object
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
    [SerializeField]
    private float BoostDurationInSeconds = 1;
    private bool isBoosting = false;

    //whip Variables
    [SerializeField]
    private GameObject WhipObject;
    [SerializeField]
    private float WhipCost;
    [SerializeField]
    private float WhipSlowdown;
    [SerializeField]
    private float WhipSlowdownDuration;
    [SerializeField]
    private float WhipCooldown;
    [SerializeField]
    private float WhipAttackDuration;
    private bool isWhipping = false;

    //input strings
    [SerializeField]
    private string HorizontalInputAxis;
    [SerializeField]
    private string VerticalInputAxis;
    [SerializeField]
    private string BoostInputAxis;
    [SerializeField]
    private string WhipInputAxis;

    //sprite + animation variables
    private bool isFacingRight = true;

    // Use this for initialization
    void Start()
    {

        _playerState = PlayerState.Start;
        rigidbody2D = GetComponent<Rigidbody2D>();
        WhipObject.SetActive(false);
        Speed = BaseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Boost();
        Flip();
        UpdateAnimationVariables();
    }

    //called once per physics calculation
    private void FixedUpdate()
    {
        Move();
        Whip();
    }

    private void UpdateAnimationVariables()
    {
        
    }

    //gets input from the gamepad each frame, which is passed to the Move() function
    private void GetInput()
    {
        //TODO get input from controller
        XInput = Input.GetAxis(HorizontalInputAxis);
        YInput = Input.GetAxis(VerticalInputAxis);
        BoostInput = Input.GetAxis(BoostInputAxis);
        WhipInput = Input.GetAxis(WhipInputAxis);
    }

    //flips the sprite if it is facing the wrong direction (always keeps the sprite facing the direction of movement)
    private void Flip()
    {
        if (rigidbody2D.velocity.x < 0 && isFacingRight)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (!isFacingRight && rigidbody2D.velocity.x > 0)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    //sets the object's velocity based on its inputs
    private void Move()
    {
        _playerState = PlayerState.Playing;
        //TODO move object
        rigidbody2D.velocity = new Vector2(XInput * Speed, YInput * Speed);
    }

    //As long as the object is within a trigger, it checks the tag and takes the corresponding action
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger");

        //if the trigger is water, fill up the water meter by the waterRecoverRate
        if (collision.tag == "Water")
        {
            if (waterMeterLevel < maxWaterMeter)
            {
                waterMeterLevel += waterRecoverRate;
            }

            Debug.Log("Enter Water");
        }
    }

    //if the object enters a trigger, checks the tag and takes the corresponding action
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInMud)
        {
            //if the trigger is mud, applies the mud slowdown
            if (collision.tag == "Mud")
            {
                Speed *= mudSlowDown;
                Debug.Log("Enter Mud");
                isInMud = true;
            }

            if (collision.tag == "Whip")
            {
                StartCoroutine(GotWhippedCoroutine());
            }
        }

        if (collision.tag == "Fire")
        {
            //Stops player from moving altogether 
            Speed = BaseSpeed = 0;
            _playerState = PlayerState.Dead;
            Debug.Log("Enter Fire Wall");
        }
    }

    private void Whip()
    {
        if (WhipInput > 0 && !isWhipping && waterMeterLevel > WhipCost)
        {
            isWhipping = true;
            WhipObject.SetActive(true);
            waterMeterLevel -= WhipCost;
            StartCoroutine(WhipAttackDurationCoroutine());
            StartCoroutine(WhipCooldownCoroutine());
        }

    }

    //Slowdown for a duration when whipped
    IEnumerator GotWhippedCoroutine()
    {
        Speed *= WhipSlowdown;
        yield return new WaitForSeconds(WhipSlowdownDuration);
        Speed = BaseSpeed;

    }

    IEnumerator WhipAttackDurationCoroutine()
    {
        yield return new WaitForSeconds(WhipAttackDuration);
        WhipObject.SetActive(false);
    }

    IEnumerator WhipCooldownCoroutine()
    {
        yield return new WaitForSeconds(WhipCooldown);
        isWhipping = false;
    }

    //if the object leaves a trigger, checks the tage and takes the corresponding action
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if the trigger is mud, ends the slowdown
        if (collision.tag == "Mud")
        {
            Speed = BaseSpeed;
            isInMud = false;
            Debug.Log("Exit Mud");
        }
    }

    //increases the object's speed briefly at the cost of water
    private void Boost()
    {
        if (!isBoosting && waterMeterLevel >= BoostCost && BoostInput > 0.9)
        {
            isBoosting = true;
            waterMeterLevel -= BoostCost;
            StartCoroutine(BoostCoroutine());
            StartCoroutine(BoostCooldownCoroutine());
        }
    }

    //forces the speed boost to last a set duration in real-time seconds
    IEnumerator BoostCoroutine()
    {
            Speed += BoostAmount;
            yield return new WaitForSeconds(BoostDurationInSeconds);
            Speed = BaseSpeed;
    }
    //forces a wait to boost again in real-time seconds
    IEnumerator BoostCooldownCoroutine()
    {
        yield return new WaitForSeconds(BoostCooldownInSeconds);
        isBoosting = false;
    }

}
