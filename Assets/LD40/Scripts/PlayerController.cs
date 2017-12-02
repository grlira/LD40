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
        Vector3 dir = Vector3.zero;
        if (Camera.current != null)
        {
            var mouse = Camera.current.ScreenToWorldPoint(Input.mousePosition);

            dir = (mouse - myTransform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }

        var up = new Vector3(0.0f, 0.0f, 1.0f);
        // find right vector:
        var dirRight = Vector3.Cross(dir.normalized, up.normalized);

        var characterController = GetComponent<TDCharacterController2D>();
        
        if (Input.GetKey(KeyCode.W))
        {
            characterController.Move(dir * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(-dir * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            characterController.Move(-dirRight * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            characterController.Move(dirRight * speed);
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
