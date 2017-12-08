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

    private bool dieing = false;

    // Use this for initialization
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        myRigidBody.drag = 10;

        GetComponent<AnimatedSprite>().RandomizeGroup();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Poop>() != null && destructionTime == 0)
        {
            StartCoroutine(DeathAnimation(collision.gameObject));
        }
    }

    IEnumerator DeathAnimation(GameObject poop)
    {
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<Collider2D>().enabled = false;
        poop.GetComponent<Collider2D>().enabled = false;
        dieing = true;

        var startingPos = this.transform.position;
        var targetPos = poop.transform.position;

        float t = 0;

        do
        {
            t += Time.deltaTime * 2;
            if (t > 1f) t = 1f;

            // Slip on the poop
            var rot = this.transform.rotation;
            rot.eulerAngles = new Vector3(0, 0, Mathf.SmoothStep(0, 90, t));
            this.transform.rotation = rot;

            // Move smoothly into the poop
            this.transform.position = Vector3.Lerp(startingPos, targetPos, t);

            yield return new WaitForEndOfFrame();
        }
        while (t < 1f);

        Destroy(poop);
        GameOverlordController.instance.DestroyVisitor(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (dieing)
            return;

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
