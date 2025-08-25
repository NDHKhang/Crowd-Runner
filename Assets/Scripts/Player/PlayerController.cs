using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("References")]
    [SerializeField] private CrowdSystem crowdArranger;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float roadWidth;

    [Header("Controller")]
    [SerializeField] private float slideSpeed = 8f;
    private Vector3 clickedScreenpos;
    private Vector3 clickedPlayerpos;

    private bool canMove = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onGameStateChange += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChange -= GameStateChangedCallBack;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveForward();
            ManageControl();
        }
    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Game)
            StartMoving();
    }

    private void StartMoving()
    {
        canMove = true;
        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;
        playerAnimator.Idle();
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

            // Clamp the x position prevent player go out of boundary
            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdArranger.GetCrowdRadius(),
                roadWidth / 2 - crowdArranger.GetCrowdRadius());

            transform.position = position;
        }
    }
}
