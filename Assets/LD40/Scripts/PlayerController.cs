﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int health;
    public PlayerController controller;

    public float speed;



    private Rigidbody2D myRigidBody;

    // Use this for initialization
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        myRigidBody.drag = 10;
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.current != null)
        {
            var mouse = Camera.current.ScreenToWorldPoint(Input.mousePosition);

            Vector3 dir = mouse - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    void FixedUpdate()
    {
        var velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            velocity += Vector2.up * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            velocity += Vector2.down * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            velocity += Vector2.left * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector2.right * speed;
        }

        if (velocity != Vector2.zero)
            myRigidBody.velocity = velocity;
    }



}
