using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    private Rigidbody rb;

    public float tumble;

    // Start is called before the first frame update
    void Start()
    {

        //bug >> y position increases steadily with rotation
        //will not rotate if gravity and kinematic is selected
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
