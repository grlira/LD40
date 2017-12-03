using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBounds : MonoBehaviour {

    private Collider2D myCollider;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(myCollider.bounds);
	}
}
