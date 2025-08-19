using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Chunk[] chunksPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GenerateChunks();
    }

    private void GenerateChunks()
    {
        Vector3 chunkPos = Vector3.zero;
        for (int i = 0; i < 5; i++)
        {
            Chunk chunkToCreate = chunksPrefab[Random.Range(0, chunksPrefab.Length)];

            // For ensuring the next chunk is place right after the previous chunk
            if (i > 0)
                chunkPos.z += chunkToCreate.GetLength() / 2;

            Instantiate(chunkToCreate, chunkPos, Quaternion.identity);
            chunkPos.z += chunkToCreate.GetLength() / 2;
        }
    }
}
