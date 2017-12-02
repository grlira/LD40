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
        Vector2? playerToMouse = getPlayerToMouse();

        if (playerToMouse != null)
        {
            Vector2 playerToMouseNotNull = (Vector2)playerToMouse;
            setPlayerOrientation((Vector2) playerToMouse);

            Vector2 movementDirection = Vector2.zero;
            var characterController = GetComponent<TDCharacterController2D>();

            if (Input.GetKey(KeyCode.W))
            {
                movementDirection = playerToMouseNotNull;
            }

            if (Input.GetKey(KeyCode.S))
            {
                movementDirection = -playerToMouseNotNull;
            }

            if (Input.GetKey(KeyCode.A))
            {
                movementDirection = Vector3.Cross(playerToMouseNotNull.normalized, Vector3.forward.normalized);
            }

            if (Input.GetKey(KeyCode.D))
            {
                movementDirection = -Vector3.Cross(playerToMouseNotNull.normalized, Vector3.forward.normalized);
            }

            characterController.Move(movementDirection * speed);
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

    Vector2? getPlayerToMouse()
    {
        if (Camera.current == null)
        {
            return null;
        }

        var mousePosition = Camera.current.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        return (mousePosition2D - new Vector2(myTransform.position.x, myTransform.position.y)).normalized;
    }

    void setPlayerOrientation(Vector2 playerToMouse)
    {
        float playerOrientation = Mathf.Atan2(playerToMouse.y, playerToMouse.x);
        this.transform.rotation = Quaternion.AngleAxis(playerOrientation * Mathf.Rad2Deg, Vector3.forward);
    }

}
