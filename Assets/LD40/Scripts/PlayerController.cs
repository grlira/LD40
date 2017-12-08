using Prime31;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatedSprite))]
public class PlayerController : MonoBehaviour
{
    [Serializable]
    public class OldLadySpriteSide
    {
        public Sprite[] sprites;
    }

    public float speed;

    private Rigidbody2D myRigidBody;
    private Transform myTransform;
    private AnimatedSprite myAnimatedSprite;

    private int animationFrame = 0;
    private float nextAnimationFrame;
    private float endAnimationFrame;

    private Helpers.MovementType movementType;

    // Use this for initialization
    void Start()
    {
        movementType = Helpers.GetOptionsMovementType();
    }

    void Awake()
    {
        myAnimatedSprite = GetComponent<AnimatedSprite>();
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Vector2 playerToMouse = getPlayerToMouse();

        Vector2 playerToMouseNotNull = (Vector2)playerToMouse;
        float angle = getPlayerAngle(playerToMouseNotNull);
        setPlayerSprite(angle);


        Vector2 movementDirection = Vector2.zero;
        var characterController = GetComponent<TDCharacterController2D>();

        if (movementType == Helpers.MovementType.TowardsMouse)
        {
            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += playerToMouseNotNull;
                endAnimationFrame = Time.time + 0.15f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += -playerToMouseNotNull;
                endAnimationFrame = Time.time + 0.15f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                movementDirection = Vector3.Cross(Vector3.forward.normalized, playerToMouseNotNull.normalized);
                endAnimationFrame = Time.time + 0.15f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movementDirection = -Vector3.Cross(Vector3.forward.normalized, playerToMouseNotNull.normalized);
                endAnimationFrame = Time.time + 0.15f;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += Vector2.up;
                endAnimationFrame = Time.time + 0.15f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += Vector2.down;
                endAnimationFrame = Time.time + 0.15f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                movementDirection += Vector2.left;
                endAnimationFrame = Time.time + 0.15f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movementDirection += Vector2.right;
                endAnimationFrame = Time.time + 0.15f;
            }

            
        }

        movementDirection = movementDirection * speed;

        if (movementDirection.magnitude > speed)
            movementDirection = movementDirection.normalized * speed;

        characterController.Move(movementDirection * Time.deltaTime);




        // Use items
        if (Input.GetKeyDown(KeyCode.E))
        {
            var item = GameOverlordController.instance.SelectedItem;

            if (item != null)
            {
                if (Vector2.Distance(item.transform.position, this.transform.position) < 2)
                    item.OnItemUse(this);
            }
        }

        if (Time.time < this.endAnimationFrame)
            myAnimatedSprite.isAnimated = true;
        else
            myAnimatedSprite.isAnimated = false;
    }


    void FixedUpdate()
    {
        
    }

    Vector2 getPlayerToMouse()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        return (mousePosition2D - new Vector2(myTransform.position.x, myTransform.position.y)).normalized;
    }

    float getPlayerAngle(Vector2 playerToMouse)
    {
        return Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;
    }

    void setPlayerSprite(float angle)
    {
        int index = Mathf.FloorToInt((angle + 22.5f + 180f)/45f) % 8;
        myAnimatedSprite.Group = index;
    }
}
