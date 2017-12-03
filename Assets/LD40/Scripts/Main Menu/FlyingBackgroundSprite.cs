using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBackgroundSprite : MonoBehaviour {

    public Sprite[] sprites;


    private float moveSpeed;
    private float rotateSpeed;
	// Use this for initialization
	void Start () {
        GetRandomProperties();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(moveSpeed,0) * Time.deltaTime;
        this.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if(transform.position.x >= 12.5)
            GetRandomProperties();
	}

    private void GetRandomProperties()
    {
        // Get random position and rotation
        transform.position = new Vector3(-14, Random.Range(-5.3f, 5.3f));
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        // Get random image
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

        // Get random move and rotation speed
        moveSpeed = Random.Range(2f, 8f);
        rotateSpeed = Random.Range(5f, 15f);
    }
}
