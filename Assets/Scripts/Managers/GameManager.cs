using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject playerBlock, AIBlock;
    public int spawnCount;

    public List<Vector3> blockPosForPlayer,blockPosForAi;
    public List<GameObject> playerBlocks, AIBlocks;

    public static GameManager instance;
    public List<Transform> blockSpawnPosList;
    public List<GameObject> AISpawnedBlocks;

    public bool isGameRunning;
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
        for (int i = 0; i < blockSpawnPosList[2].transform.childCount; i++)
        {
            GameObject clonePlayerBlock = Instantiate(playerBlock, blockSpawnPosList[2].GetChild(i).transform.position, Quaternion.identity);
            clonePlayerBlock.SetActive(true);
        }
        for (int i = 0; i < blockSpawnPosList[3].transform.childCount; i++)
        {
            GameObject cloneAIBlock = Instantiate(AIBlock, blockSpawnPosList[3].GetChild(i).transform.position, Quaternion.identity);
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
                int playerBlockPos = Random.Range(0, blockPosForPlayer.Count);

                if (blockPosForPlayer.Count > 0)
                {
                    GameObject clonePlayerBlock = Instantiate(playerBlock, blockPosForPlayer[playerBlockPos], Quaternion.identity);
                    clonePlayerBlock.transform.parent = transform;
                    playerBlocks.Add(clonePlayerBlock);
                    blockPosForPlayer.RemoveAt(playerBlockPos);
                    clonePlayerBlock.SetActive(true);
                }
            }
            reSpawnAIBlocks = spawnCount - AIBlocks.Count;
            for (int i = 0; i < reSpawnAIBlocks; i++)
            {
                int AIBlockPos = Random.Range(0, blockPosForAi.Count);
                if (blockPosForAi.Count > 0)
                {
                    GameObject cloneAIBlock = Instantiate(AIBlock, blockPosForAi[AIBlockPos], Quaternion.identity);
                    cloneAIBlock.transform.parent = transform;
                    AISpawnedBlocks.Add(cloneAIBlock);
                    AIBlocks.Add(cloneAIBlock);
                    blockPosForAi.RemoveAt(AIBlockPos);
                    cloneAIBlock.SetActive(true);
                }
            }
            int reSpawnPlayerBlocks2, reSpawnAIBlocks2;
            reSpawnPlayerBlocks2 = spawnCount - playerBlocks.Count;
            for (int i = 0; i < reSpawnPlayerBlocks2; i++)
            {
                int playerBlockPos = Random.Range(0, blockPosForPlayer.Count);

                if (blockPosForPlayer.Count > 0)
                {
                    GameObject clonePlayerBlock = Instantiate(playerBlock, blockPosForPlayer[playerBlockPos], Quaternion.identity);
                    clonePlayerBlock.transform.parent = transform;
                    playerBlocks.Add(clonePlayerBlock);
                    blockPosForPlayer.RemoveAt(playerBlockPos);
                    clonePlayerBlock.SetActive(true);
                }
            }
            reSpawnAIBlocks2 = spawnCount - AIBlocks.Count;
            for (int i = 0; i < reSpawnAIBlocks2; i++)
            {
                int AIBlockPos = Random.Range(0, blockPosForAi.Count);
                if (blockPosForAi.Count > 0)
                {
                    GameObject cloneAIBlock = Instantiate(AIBlock, blockPosForAi[AIBlockPos], Quaternion.identity);
                    cloneAIBlock.transform.parent = transform;
                    AISpawnedBlocks.Add(cloneAIBlock);
                    AIBlocks.Add(cloneAIBlock);
                    blockPosForAi.RemoveAt(AIBlockPos);
                    cloneAIBlock.SetActive(true);
                }
            }
        } while (true);
    }

    public float CalculateAngleBetweenTwoTransform(Transform aTransform, Transform bTransform)
    {
        return Vector3.Angle(aTransform.forward, bTransform.transform.position - aTransform.position);
    }
}