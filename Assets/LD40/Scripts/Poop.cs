using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : ItemBase
{

    public Sprite sprite1, sprite2;

    public GameObject cleaningPrefab;

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
        StartCoroutine(CleaningAnimation());
        
    }

    private IEnumerator CleaningAnimation()
    {
        this.GetComponent<Collider2D>().enabled = false;
        var go = Instantiate(cleaningPrefab);
        go.transform.position = transform.position;

        yield return new WaitForSeconds(1.5f);

        Destroy(go);
        Destroy(this.gameObject);
    }
}
