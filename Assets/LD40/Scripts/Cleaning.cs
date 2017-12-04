using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour {

    public Sprite[] sprites;

    private float nextAnimationFrame;
    private int animationFrame;

	// Use this for initialization
	void Start () {
        nextAnimationFrame = Time.time;
        animationFrame = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= nextAnimationFrame)
        {
            animationFrame++;

            if (animationFrame >= sprites.Length)
                animationFrame = 0;

            nextAnimationFrame += 0.2f;
            GetComponent<SpriteRenderer>().sprite = sprites[animationFrame];
            
        }
	}
}
