using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private CrowdSystem crowdArranger;

    // Update is called once per frame
    void Update()
    {
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
                doors.Disable();

                int doorAmount = doors.GetDoorAmount(transform.position.x);
                DoorType doorType = doors.GetDoorType(transform.position.x);

                crowdArranger.ApplyAmount(doorType, doorAmount);
            }
        }
    }
}
