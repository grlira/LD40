using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : ItemBase
{

    public Sprite sprite1, sprite2;

    private float nextAnimation;

    private void Update()
    {
        if (Time.time > nextAnimation)
        {
            nextAnimation += 1;

            var renderer = this.GetComponent<SpriteRenderer>();
            if (renderer.sprite == sprite1)
            {
                renderer.sprite = sprite2;
            }
            else
            {
                renderer.sprite = sprite1;
            }
        }
    }

    public override void OnItemUse(PlayerController player)
    {
        Destroy(this.gameObject);
    }
}
