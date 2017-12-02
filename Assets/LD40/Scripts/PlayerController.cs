using Prime31;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myRigidBody;
    private Transform myTransform;

    // Use this for initialization
    void Start()
    {
        myTransform = this.transform;
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

            Vector3 dir = mouse - myTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }


        var characterController = GetComponent<TDCharacterController2D>();
        
        if (Input.GetKey(KeyCode.W))
        {
            characterController.Move(Vector2.up * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(Vector2.down * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            characterController.Move(Vector2.left * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            characterController.Move(Vector2.right * speed);
        }
    }


    void FixedUpdate()
    {
        

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Poop>() != null)
        {
            Destroy(collision.gameObject);
        }   
    }



}
