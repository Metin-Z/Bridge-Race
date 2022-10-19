using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject playerBlock, AIBlock;
    public int spawnCount;

    public List<Vector3> BlockPos;
    public List<GameObject> playerBlocks, AIBlocks;

    public static GameManager instance;
    public List<Transform> blockSpawnPosList;
    public List<GameObject> AISpawnedBlocks;
    public virtual void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        StartCoroutine(SpawnBlockFirst());
    }
    public IEnumerator SpawnBlockFirst()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < blockSpawnPosList[0].transform.childCount; i++)
        {
            GameObject clonePlayerBlock = Instantiate(playerBlock, blockSpawnPosList[0].GetChild(i).transform.position, Quaternion.identity);
            clonePlayerBlock.SetActive(true);
        }
        for (int i = 0; i < blockSpawnPosList[1].transform.childCount; i++)
        {
            GameObject cloneAIBlock = Instantiate(AIBlock, blockSpawnPosList[1].GetChild(i).transform.position, Quaternion.identity);
            AISpawnedBlocks.Add(cloneAIBlock);
            cloneAIBlock.SetActive(true);
        }
        StartCoroutine(SpawnBlock());
    }
    public IEnumerator SpawnBlock()
    {
        do
        {
            yield return new WaitForSeconds(7);

            int reSpawnPlayerBlocks, reSpawnAIBlocks;
            reSpawnPlayerBlocks = spawnCount - playerBlocks.Count;

            for (int i = 0; i < reSpawnPlayerBlocks; i++)
            {
                int playerBlockPos = Random.Range(0, BlockPos.Count);

                if (BlockPos.Count > 0)
                {
                    GameObject clonePlayerBlock = Instantiate(playerBlock, BlockPos[playerBlockPos], Quaternion.identity);
                    clonePlayerBlock.transform.parent = transform;
                    playerBlocks.Add(clonePlayerBlock);
                    BlockPos.RemoveAt(playerBlockPos);
                    clonePlayerBlock.SetActive(true);
                }
            }
            reSpawnAIBlocks = spawnCount - AIBlocks.Count;
            for (int i = 0; i < reSpawnAIBlocks; i++)
            {
                int AIBlockPos = Random.Range(0, BlockPos.Count);
                if (BlockPos.Count > 0)
                {
                    GameObject cloneAIBlock = Instantiate(AIBlock, BlockPos[AIBlockPos], Quaternion.identity);
                    cloneAIBlock.transform.parent = transform;
                    AISpawnedBlocks.Add(cloneAIBlock);
                    AIBlocks.Add(cloneAIBlock);
                    BlockPos.RemoveAt(AIBlockPos);
                    cloneAIBlock.SetActive(true);
                }
            }
        } while (true);
    }
}