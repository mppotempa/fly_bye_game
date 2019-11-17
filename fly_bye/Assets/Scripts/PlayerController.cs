using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject shot;
    public Transform shotSpawn;
    public float speed;
    public float tilt;

    //limits shots per sec DELETE LATER
    public float fireRate;
    private float nextFire;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVerticle = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVerticle);
        rb.velocity = movement * speed;

        //limits the player's movement to the screen, regardless of screen size
        //var pos = Camera.main.WorldToViewportPoint(transform.position);
        //pos.x = Mathf.Clamp(pos.x, 0.09f, 0.91f);
        //pos.y = Mathf.Clamp(pos.y, 0.09f, 0.91f);
        //transform.position = Camera.main.ViewportToWorldPoint(pos);

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    private void Update()
    {
        if (Input.GetButton("Shoot") && Time.time > nextFire)
        {
            //CHANGE LATER TO ADJUST FOR FUEL
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }
}
