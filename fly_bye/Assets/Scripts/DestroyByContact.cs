using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    PlayerController pc;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("EnemyShot"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        
        if(other.CompareTag("Player"))
        {
            if (this.CompareTag("EnemyShot") && pc.sheild > 0)
            {
                //decrease sheild if player is hit by enemy shot
                print("Enemy Fire");
                //print("Current Shield Level: " + pc.sheild);
                pc.sheild = pc.sheild - 1;
                print("Sheild Levels: " + pc.sheild);
                Destroy(gameObject);
                return;
            }
            else
            {
                print("Collision");
                Instantiate(playerExplosion, transform.position, transform.rotation);
            }
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
