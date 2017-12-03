using UnityEngine;
using System.Collections;

public class VisitorController : MonoBehaviour
{
    const int VISITOR_MAX_MOVE_TRESHOLD = 75;
    const int DIRECTION_DOWN = 0;
    const int DIRECTION_UP = 1;
    const int DIRECTION_LEFT = 2;
    const int DIRECTION_RIGHT = 3;

    public float speed;

    private Rigidbody2D myRigidBody;
    private float destructionTime = 0;


    // Use this for initialization
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        myRigidBody.drag = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (destructionTime > 0 && destructionTime <= Time.time)
        {
            GameOverlordController.instance.DestroyVisitor(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Poop>() != null && destructionTime == 0)
        {
            this.GetComponent<AudioSource>().Play();
            destructionTime = Time.time + 1f;
        }
    }

    private void FixedUpdate()
    {
        int movementDirection = Mathf.FloorToInt(Random.Range(0, VISITOR_MAX_MOVE_TRESHOLD));

        Vector2 movementVector = Vector2.zero;

        switch (movementDirection)
        {
            case DIRECTION_DOWN:
                movementVector = Vector2.down;
                break;
            case DIRECTION_UP:
                movementVector = Vector2.up;
                break;
            case DIRECTION_LEFT:
                movementVector = Vector2.left;
                break;
            case DIRECTION_RIGHT:
                movementVector = Vector2.right;
                break;
        }

        myRigidBody.velocity += movementVector * speed;
    }
}
