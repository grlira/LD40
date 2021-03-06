﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject tracking;

    private Camera myCamera;

    private void Awake()
    {
        myCamera = this.GetComponent<Camera>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (tracking == null)
            return;

        var mypos = this.transform.position;

        mypos.x = tracking.transform.position.x;
        mypos.y = tracking.transform.position.y;

        this.transform.position = mypos;
    }

    
}
