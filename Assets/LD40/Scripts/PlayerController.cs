using Prime31;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myRigidBody;
    private Transform myTransform;

    public Sprite[] playerSprites;

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

    private void SetSprite(int side)
    {
        this.GetComponent<SpriteRenderer>().sprite = playerSprites[side];
    }

    // Update is called once per frame
    void Update()
    {
        float angle = 0f;
        if (Camera.main != null)
        {
            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 dir = mouse - myTransform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }


        if (angle < 0) angle = (360 - Mathf.Abs(angle - 180)) + 180;

        if (angle > 337.5 || angle <= 22.5)
            SetSprite(2);
        else if (angle > 22.5 && angle <= 67.5)
            SetSprite(1);
        else if (angle > 67.5 && angle <= 112.5)
            SetSprite(0);
        else if (angle > 112.5 && angle <= 157.5)
            SetSprite(7);
        else if (angle > 157.5 && angle <= 202.5)
            SetSprite(6);
        else if (angle > 202.5 && angle <= 247.5)
            SetSprite(5);
        else if (angle > 247.5 && angle <= 292.5)
            SetSprite(4);
        else if (angle > 292.5 && angle <= 337.5)
            SetSprite(3);

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
