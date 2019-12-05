using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    PlayerController pc;
    GameController control;

    private void Start()
    {

        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        control = controller.GetComponent<GameController>();
        try {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            pc = player.GetComponent<PlayerController>();
        }
        catch (Exception e)
        {
            return;
        }
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
                //print("Enemy Fire");
                //print("Current Shield Level: " + pc.sheild);
                pc.sheild = pc.sheild - 1;
                //print("Sheild Levels: " + pc.sheild);
                Destroy(gameObject);
                control.UpdateShield(pc.sheild);
                return;
            }
            else
            {
                control.UpdateShield(0);
                //print("Collision");
                Instantiate(playerExplosion, transform.position, transform.rotation);
                control.EndGame();
            }
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
