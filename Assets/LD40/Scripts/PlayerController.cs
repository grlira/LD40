using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int health;
    public PlayerController controller;



    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            var characterController = this.GetComponent<CharacterController>();

            characterController.Move(Vector2.down * Time.deltaTime);
        }


    }


    void FixedUpdate()
    {

    }



}
