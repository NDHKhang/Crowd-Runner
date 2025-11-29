using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Collider[] detectColliders = new Collider[10];
    private Vector3 boxSize;

    private bool isRegisted = false; // Prevents registering every frame\

    private void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxSize = transform.lossyScale;
    }

    private void Update()
    {
        HandleCollision();
    }

    private void HandleCollision()
    {
        int numColliders = Physics.OverlapBoxNonAlloc(
            transform.position,
            boxSize / 2f, // halfExtents
            detectColliders,
            transform.rotation,
            LayerMask.GetMask("Player")
        );

        bool isColliding = numColliders > 0;

        if (isColliding && !isRegisted)
        {
            CrowdSystem.Instance.RegisterObstacle(this);
            isRegisted = true;
        }
        else if (!isColliding && isRegisted)
        {
            StartCoroutine(UnregisterAfterDelay(1.5f)); // Delay before allowing reposition
            isRegisted = false;
        }

        for (int i = 0; i < numColliders; i++)
        {
            Destroy(detectColliders[i].gameObject);
        }
    }

    private IEnumerator UnregisterAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CrowdSystem.Instance.UnregisterObstacle(this);
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
