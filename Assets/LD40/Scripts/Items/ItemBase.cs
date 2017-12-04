using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {


    public virtual void OnItemSelected()
    {
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public virtual void OnItemDeselected()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void OnItemUse(PlayerController player)
    {

    }
}
