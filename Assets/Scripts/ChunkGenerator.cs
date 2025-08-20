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
        for (int i = 0; i < chunksPrefab.Length; i++)
        {
            // For ensuring the next chunk is place right after the previous chunk
            if (i > 0)
                chunkPos.z += chunksPrefab[i].GetLength() / 2;

            Instantiate(chunksPrefab[i], chunkPos, Quaternion.identity, transform);
            chunkPos.z += chunksPrefab[i].GetLength() / 2;
        }
    }
}
