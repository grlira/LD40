using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject tracking;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var mypos = this.transform.position;

        mypos.x = tracking.transform.position.x;
        mypos.y = tracking.transform.position.y;

        this.transform.position = mypos;
    }
}
