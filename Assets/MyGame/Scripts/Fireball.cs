using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float MoveSpeed;
    public GameObject CollidedWithSomethingParticleSystem;
    public MeshRenderer MeshRenderer;
    public CapsuleCollider Collider;
    public Rigidbody rb;
    public float TimeToDestroy = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        gameObject.transform.Rotate(90,0,0);
        rb.AddForce(rb.transform.up * MoveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Spawn the explosion with colliding with something particle system.
        if (collision.collider.CompareTag("Wall"))
        {
            //disable the mesh renderer so the trail of the fireball continues without the object there
            MeshRenderer.enabled = false;
            //disable the collider to avoid all other instances of collisions.
            Collider.enabled = false;
            //destroy the object after certain amount of seconds.
            Destroy(gameObject);
        }
    }
}