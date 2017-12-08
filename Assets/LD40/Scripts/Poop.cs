using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : ItemBase
{

    public GameObject cleaningPrefab;
    public GameObject outline;

    private void Start()
    {
    }

    private void Update()
    {
        
    }

    public override void OnItemSelected()
    {
        outline.SetActive(true);
    }

    public override void OnItemDeselected()
    {
        outline.SetActive(false);
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

        yield return new WaitForSeconds(3f);

        Destroy(go);
        Destroy(this.gameObject);
    }
}
