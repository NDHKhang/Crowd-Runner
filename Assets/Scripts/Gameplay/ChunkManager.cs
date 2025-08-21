using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header("Prefabs")]
    [SerializeField] private LevelSO[] levelSO;
    private GameObject finishLine;

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
        Init();
    }

    private void Init()
    {
        GenerateLevel();

        finishLine = GameObject.FindWithTag("Finish");
    }

    private void GenerateLevel()
    {
        int currentLevel = GetLevel();

        // Clamp the level so the game endless and avoid the level greater than the levelSO length
        currentLevel = currentLevel % levelSO.Length;

        LevelSO level = levelSO[currentLevel];
        GenerateLevel(level.chunks);
    }

    private void GenerateLevel(Chunk[] levelChunks)
    {
        Vector3 chunkPos = Vector3.zero;
        for (int i = 0; i < levelChunks.Length; i++)
        {
            // For ensuring the next chunk is place right after the previous chunk
            if (i > 0)
                chunkPos.z += levelChunks[i].GetLength() / 2;

            Instantiate(levelChunks[i], chunkPos, Quaternion.identity, transform);
            chunkPos.z += levelChunks[i].GetLength() / 2;
        }
    }

    //private void GenerateRandomLevel()
    //{
    //    Vector3 chunkPos = Vector3.zero;
    //    for (int i = 0; i < levelChunk.Length; i++)
    //    {
    //        Chunk chunkToCreate = levelChunk[Random.Range(0, levelChunk.Length)];

    //        // For ensuring the next chunk is place right after the previous chunk
    //        if (i > 0)
    //            chunkPos.z += chunkToCreate.GetLength() / 2;

    //        Instantiate(chunkToCreate, chunkPos, Quaternion.identity, transform);
    //        chunkPos.z += chunkToCreate.GetLength() / 2;
    //    }
    //}

    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("level", 0);
    }
}
