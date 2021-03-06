﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    GameController control;

    public Boundary boundary;
    private Rigidbody rb;
    public GameObject shot;
    public Transform shotSpawn;
    public float tilt;

    public float power;
    public int shots;
    public int sheild;

    float maxSpeed;

    //limits shots per sec
    public float fireRate;
    private float nextFire;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxSpeed = 30f;
        sheild = 10;
        power = 1.0f;
        shots = 10;
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        control = controller.GetComponent<GameController>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVerticle = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVerticle);
        rb.velocity = movement * (maxSpeed * power);


        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );


        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

    }

    private void Update()
    {
        if (Input.GetButton("Shoot") && Time.time > nextFire)
        {
            if(shots > 0)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                shots--;
                power -= 0.1f;
                control.UpdateLevel(power);
                //print("Shots: " + shots + " Power: " + power);
            }
            else
            {
                return;
            }
        }


    }
}
