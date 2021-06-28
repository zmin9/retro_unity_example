using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;  //bullet rigidbody component
    public float speed = 8f;    //bullet speed

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>(); //find rigidbody component
        bulletRigidbody.velocity = transform.forward * speed;

        Destroy(gameObject, 3f); //3seconds later, destroy this bullet object
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") //check 'other' is player using tag
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if(playerController!=null) //prevent error
            {
                playerController.Die();
            }
        }
    }
}