using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CrowdSystem crowdArranger;

    [Header("Event")]
    public static Action onDoorHit;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.IsGameState())
            HandleDoorCollision();    
    }

    private void HandleDoorCollision()
    {
        // Detect colliders in a radius of 1 around the player
        Collider[] detectColliders = Physics.OverlapSphere(transform.position, 1);

        for (int i = 0; i < detectColliders.Length; i++)
        {
            if (detectColliders[i].TryGetComponent(out Doors doors))
            {
                // Prevent re-triggering by disabling the door
                doors.Disable(transform.position.x);

                onDoorHit?.Invoke();

                int doorAmount = doors.GetDoorAmount(transform.position.x);
                DoorType doorType = doors.GetDoorType(transform.position.x);

                crowdArranger.ApplyAmount(doorType, doorAmount);
            }
            else if (detectColliders[i].CompareTag("Finish"))
            {
                // Update level, state and add coin when finish
                SaveLoadManager.SaveInt("level", ChunkManager.instance.GetLevel() + 1);
                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);

                DataManager.instance.AddCoins(10);
            }
        }
    }
}
