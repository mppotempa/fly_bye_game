using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVerticle = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVerticle);
        rb.velocity = movement * (maxSpeed * power);

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);


        //limits the player's movement to the screen, regardless of screen size
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        //pos.x = Mathf.Clamp(pos.x, 0.09f, 0.91f);
        //pos.y = Mathf.Clamp(pos.y, 0.09f, 0.91f);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        //set z to the camera distance from z 0
        pos.z = Camera.main.transform.position.z;
        Vector3 world = Camera.main.ViewportToWorldPoint(pos);
        world.y = transform.position.y;
        transform.position = world;
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
                print("Shots: " + shots + " Power: " + power);
            }
            else
            {
                return;
            }
        }


    }
}
