using System.Collections;
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

    }


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myRigidBody.velocity = Vector2.up * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            myRigidBody.velocity = Vector2.down * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            myRigidBody.velocity = Vector2.left * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            myRigidBody.velocity = Vector2.right * speed;
        }
        

    }



}
