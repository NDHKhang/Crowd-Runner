using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    //[SerializeField] private Chunk[] chunksPrefab;
    [SerializeField] private Chunk[] levelChunk;

    // Start is called before the first frame update
    void Start()
    {
        GenerateOrderedLevel();
    }

    private void GenerateOrderedLevel()
    {
        Vector3 chunkPos = Vector3.zero;
        for (int i = 0; i < levelChunk.Length; i++)
        {
            // For ensuring the next chunk is place right after the previous chunk
            if (i > 0)
                chunkPos.z += levelChunk[i].GetLength() / 2;

            Instantiate(levelChunk[i], chunkPos, Quaternion.identity, transform);
            chunkPos.z += levelChunk[i].GetLength() / 2;
        }
    }

    private void GenerateRandomLevel()
    {
        Vector3 chunkPos = Vector3.zero;
        for (int i = 0; i < levelChunk.Length; i++)
        {
            Chunk chunkToCreate = levelChunk[Random.Range(0, levelChunk.Length)];

            // For ensuring the next chunk is place right after the previous chunk
            if (i > 0)
                chunkPos.z += chunkToCreate.GetLength() / 2;

            Instantiate(chunkToCreate, chunkPos, Quaternion.identity, transform);
            chunkPos.z += chunkToCreate.GetLength() / 2;
        }
    }
}
