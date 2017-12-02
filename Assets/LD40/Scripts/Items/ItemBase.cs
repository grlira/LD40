using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

    private void OnMouseEnter()
    {
        GameOverlordController.instance.SetSelectedItem(this);

        this.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    private void OnMouseExit()
    {
        GameOverlordController.instance.ClearSelectedItem(this);

        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void OnItemUse(PlayerController player)
    {

    }
}
