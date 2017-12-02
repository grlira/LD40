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
            float angle = getPlayerAngle(playerToMouseNotNull);
            setPlayerSprite(angle);

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




        // Use items
        if(Input.GetKey(KeyCode.E))
        {
            var item = GameOverlordController.instance.SelectedItem;

            if (item != null)
            {
                if (Vector2.Distance(item.transform.position, this.transform.position) < 2)
                    item.OnItemUse(this);
            }
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
