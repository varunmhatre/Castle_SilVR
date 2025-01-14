﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    public bool canMove;

    private float camRotRad;
    Animator playerAnimation;

    // Use this for initialization
    void Start()
    {

        player = this.gameObject;
        playerAnimation = GetComponent<Animator>();

        //Get Roatation (Radians) of the camera to alter player motion
        //We use Mathf.Sin and Mathf.Cos to alter player motion which takes Radians as input
        camRotRad = Mathf.Deg2Rad * Camera.main.transform.eulerAngles.y;
        //Debug.Log(camRot);

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //dev is keyboard
            MovePlayer();

            //MR Controls
            MovePlayerStick();
        }
            

    }

    // uses arrow keys to shift the GameObject identified as "Player" by the speed.
    void MovePlayer()
    {
        //Update Roatation of the camera to alter player motion
        camRotRad = Mathf.Deg2Rad * Camera.main.transform.eulerAngles.y;

        //Moves player in camera's POV using Mathf.Sin and Mathf.Cos of the cam rotation
        Vector3 current = player.transform.position;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            SoundManager.Instance.PlaySfxOnce("wood-creak-02", 1);
            playerAnimation.SetBool("isMoving", true);
            current += new Vector3(speed * Mathf.Cos(camRotRad), 0, -speed * Mathf.Sin(camRotRad));
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            SoundManager.Instance.PlaySfxOnce("wood-creak-02", 1);
            playerAnimation.SetBool("isMoving", true);
            current += new Vector3(-speed * Mathf.Cos(camRotRad), 0, speed * Mathf.Sin(camRotRad));
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            SoundManager.Instance.PlaySfxOnce("wood-creak-02", 1);
            playerAnimation.SetBool("isMoving", true);
            current += new Vector3(-speed * Mathf.Sin(camRotRad), 0, -speed * Mathf.Cos(camRotRad));
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            SoundManager.Instance.PlaySfxOnce("wood-creak-02", 1);
            playerAnimation.SetBool("isMoving", true);
            current += new Vector3(speed * Mathf.Sin(camRotRad), 0, speed * Mathf.Cos(camRotRad));
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            playerAnimation.SetBool("isMoving", false);
            playerAnimation.Play("Idle");
            SoundManager.Instance.StopSfx();
        }
        player.transform.position = current;
    }

    //Theoretically uses an axis to move by an amount relative to the amount of stick motion.
    void MovePlayerStick()
    {
        //make player look same dir as cam
        //Vector3 rotDifference = Camera.main.transform.rotation.eulerAngles - player.transform.rotation.eulerAngles; //get difference bteween the cam and player
        //rotDifference = new Vector3(0, rotDifference.y, 0); //get rid of x and z rotations
        //player.transform.Rotate(rotDifference); //apply to player

        //current pos
        Vector3 current = player.transform.position;
        
        //get x and z
        float xAxis = Input.GetAxis("LStick_Horizontal");
        float zAxis = Input.GetAxis("LStick_Vertical");

        if (xAxis != 0 || zAxis != 0)
        {
            SoundManager.Instance.PlaySfxOnce("wood-creak-02", 5);
            playerAnimation.SetBool("isMoving", true);
        }
        else
        {
            SoundManager.Instance.StopSfx();
            playerAnimation.SetBool("isMoving", false);
            playerAnimation.Play("Idle");
        }

        //Debug.Log(xAxis +"  "+ zAxis);

        //update pos
        //current -= new Vector3(speed * xAxis, 0, speed * zAxis);
        current -= new Vector3(speed * xAxis * Mathf.Cos(camRotRad), 0, -speed * xAxis * Mathf.Sin(camRotRad));
        current -= new Vector3(speed * zAxis * Mathf.Sin(camRotRad), 0, speed * zAxis * Mathf.Cos(camRotRad));

        player.transform.LookAt(current); //look where youre going 
        
        player.transform.position = current;

    }

}