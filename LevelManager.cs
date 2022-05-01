using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    [SerializeField] List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    [SerializeField] Transform levelStartPoint;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GenerateInitialBlocks();
    }

    public void AddLevelBlock()
    {
        int randomBlock = Random.Range(0, allTheLevelBlocks.Count);
        LevelBlock block;
        Vector3 spawnPoint;
        if (currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allTheLevelBlocks[1]);
            spawnPoint = levelStartPoint.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlocks[randomBlock]);
            spawnPoint = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
        }
        block.transform.parent = transform;
        Vector3 correction = new Vector3(spawnPoint.x - block.startPoint.position.x, 
                                         spawnPoint.y - block.startPoint.position.y, 
                                         0);
        block.transform.position = correction;
        currentLevelBlocks.Add(block);
    }

    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }

    public void RemoveLevelBlock()
    {
        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllTheLevelBlocks()
    {
        while (currentLevelBlocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }
}
