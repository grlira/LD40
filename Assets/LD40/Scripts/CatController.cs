using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour
{
    const int CAT_MAX_MOVE_TRESHOLD = 75;
    const int DIRECTION_DOWN = 0;
    const int DIRECTION_UP = 1;
    const int DIRECTION_LEFT = 2;
    const int DIRECTION_RIGHT = 3;

    public float speed;

    private Rigidbody2D myRigidBody;

    public GameObject poopPrefab;


    private float nextPoop;

    // Use this for initialization
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        myRigidBody.drag = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        int movementDirection = Mathf.FloorToInt(Random.Range(0, CAT_MAX_MOVE_TRESHOLD));

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

        CheckPooping();
    }

    private void CheckPooping()
    {
        if (Time.time < nextPoop)
            return;

        // Spawn poop prefab
        Instantiate(poopPrefab, this.transform.position, Quaternion.identity);


        nextPoop = Time.time + Random.Range(5f, 15f);
    }
}
