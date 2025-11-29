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
    public static Action onPlayerDead;

    private Collider[] detectColliders = new Collider[10];

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGameState())
            HandleDoorCollision();    
    }

    private void HandleDoorCollision()
    {
        // Detect colliders in a radius of 1 around the player
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, 1f, detectColliders, LayerMask.GetMask("Door", "Finish"));

        for (int i = 0; i < numColliders; i++)
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
                SaveLoadManager.SaveInt("level", ChunkManager.Instance.GetLevel() + 1);
                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
            }
        }
    }
}
