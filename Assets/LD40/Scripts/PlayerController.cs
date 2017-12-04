using Prime31;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public OldLadySpriteSide[] playerSprites;

    private int animationFrame = 0;
    private float nextAnimationFrame;
    private float endAnimationFrame;

    private Helpers.MovementType movementType;

    // Use this for initialization
    void Start()
    {
        myTransform = this.transform;

        movementType = Helpers.GetOptionsMovementType();
    }

    void Awake()
    {

    }

    private void SetSprite(int side)
    {
        this.GetComponent<SpriteRenderer>().sprite = playerSprites[side].sprites[animationFrame];
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
                endAnimationFrame = Time.time + 0.2f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += -playerToMouseNotNull;
                endAnimationFrame = Time.time + 0.2f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                movementDirection = Vector3.Cross(Vector3.forward.normalized, playerToMouseNotNull.normalized);
                endAnimationFrame = Time.time + 0.2f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movementDirection = -Vector3.Cross(Vector3.forward.normalized, playerToMouseNotNull.normalized);
                endAnimationFrame = Time.time + 0.2f;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += Vector2.up;
                endAnimationFrame = Time.time + 0.2f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += Vector2.down;
                endAnimationFrame = Time.time + 0.2f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                movementDirection += Vector2.left;
                endAnimationFrame = Time.time + 0.2f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movementDirection += Vector2.right;
                endAnimationFrame = Time.time + 0.2f;
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

        var isAnimating = false;
        if (Time.time < this.endAnimationFrame)
            isAnimating = true;

        if (isAnimating && Time.time > this.nextAnimationFrame)
        {
            this.animationFrame++;
            this.nextAnimationFrame = Time.time + 0.25f;

            if(this.animationFrame >= 8)
                this.animationFrame = 0;
        }
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
        SetSprite(index);
    }
}
