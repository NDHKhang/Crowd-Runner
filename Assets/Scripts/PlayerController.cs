using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private float moveSpeed;

    [Header("Controller Settings")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedScreenpos;
    private Vector3 clickedPlayerpos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        ManageControl();
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private void ManageControl()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Save the click position on the screen
            clickedScreenpos = Input.mousePosition;

            // Save the player position at that moment
            clickedPlayerpos = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            // Calculating the distance move horizontally
            float xScreenDifference = Input.mousePosition.x - clickedScreenpos.x;
            xScreenDifference /= Screen.width;

            Vector3 position = transform.position;
            position.x = clickedPlayerpos.x + xScreenDifference * slideSpeed;
            transform.position = position;
        }
    }
}
