using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour {
    public AnimatedSpriteObject[] animatedSprite;
    public bool isAnimated;
    public int startingGroup;

    private SpriteRenderer mySpriteRenderer;
    private float fps, nextFrame;
    private int currentFrame, currentGroup;

    public int Frame
    {
        get
        {
            return currentFrame;
        }

        set
        {
            SetSprite(value);
        }
    }

    public int Group
    {
        get
        {
            return currentGroup;
        }

        set
        {
            currentGroup = value;

            fps = 1f / animatedSprite[currentGroup].framesPerSecond;
            SetSprite(currentFrame);
        }
    }

    public int GroupCount
    {
        get
        {
            return this.animatedSprite.Length;
        }
    }

    private void Start()
    {
        this.Group = startingGroup;
        nextFrame = Time.time + fps;
    }


    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (!isAnimated)
            return;

        if (Time.time < nextFrame)
            return;

        if (++currentFrame >= animatedSprite[currentGroup].frames.Length)
            currentFrame = 0;
        
        nextFrame = Time.time + fps;
    }

    private void SetSprite(int frame,bool changeCurrentFrame=true)
    {
        if(changeCurrentFrame)
            currentFrame = frame;

        mySpriteRenderer.sprite = animatedSprite[currentGroup].frames[currentFrame];
    }


    public void RandomizeGroup()
    {
        Group = Random.Range(0, GroupCount);
    }
}
