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
        Vector2? playerToMouse = getPlayerToMouse();

        if (playerToMouse != null)
        {
            Vector2 playerToMouseNotNull = (Vector2)playerToMouse;
            float angle = getAngleFromPlayerToMouse(playerToMouseNotNull);
            setPlayerOrientation(angle);

            Vector2 movementDirection = Vector2.zero;
            var characterController = GetComponent<TDCharacterController2D>();

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
        
            if (Input.GetKey(KeyCode.W))
            {
                characterController.Move(Vector2.up * speed);
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

    float getAngleFromPlayerToMouse(Vector2 playerToMouse)
    {
        return Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;
    }

    void setPlayerOrientation(float angle)
    {
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
