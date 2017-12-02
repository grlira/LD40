using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : ItemBase {

    public override void OnItemUse(PlayerController player)
    {
        Destroy(this.gameObject);
    }
}
