using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject playerBlock, AIBlock;
    public int spawnCount;

    public List<GameObject> playerBlocks,AIBlocks;

    public static GameManager instance;
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
        float randomX1, randomZ1;
        float randomX2, randomZ2;       
    
        for (int i = 0; i < spawnCount; i++)
        {
            randomX1 = Random.Range(-9.5f, 9.5f);
            randomZ1 = Random.Range(-9.5f, 9.5f);

            randomX2 = Random.Range(-9.5f, 9.5f);
            randomZ2 = Random.Range(-9.5f, 9.5f);

            Vector3 RandomPos1 = new Vector3(randomX1, 0.2f, randomZ1);
            Vector3 RandomPos2 = new Vector3(randomX2, 0.2f, randomZ2);

            GameObject clonePlayerBlock = Instantiate(playerBlock, RandomPos1, Quaternion.identity);
            GameObject cloneAIBlock = Instantiate(AIBlock, RandomPos2, Quaternion.identity);

            SpawnBlock(clonePlayerBlock, cloneAIBlock);        
        }
        StartCoroutine(SpawnBlock());
    }
    public IEnumerator SpawnBlock()
    {
        yield return new WaitForSeconds(7);
        float randomX1, randomZ1;
        int reSpawnPlayerBlocks,reSpawnAIBlocks;
        reSpawnPlayerBlocks = spawnCount - playerBlocks.Count;

        for (int i = 0; i < reSpawnPlayerBlocks ; i++)
        {
            randomX1 = Random.Range(-9.5f, 9.5f);
            randomZ1 = Random.Range(-9.5f, 9.5f);

            Vector3 RandomPos1 = new Vector3(randomX1, 0.2f, randomZ1);
            GameObject clonePlayerBlock = Instantiate(playerBlock, RandomPos1, Quaternion.identity);
            clonePlayerBlock.transform.parent = transform;
            playerBlocks.Add(clonePlayerBlock);
            clonePlayerBlock.transform.localScale = new Vector3(0.4f, 0.1f, 0.15f);
            Vector3 scalePlayer = new Vector3(
                clonePlayerBlock.transform.localScale.x * 2f,
                clonePlayerBlock.transform.localScale.y * 2f,
                clonePlayerBlock.transform.localScale.z * 2f);
            clonePlayerBlock.transform.DOScale(scalePlayer, 1.5f).SetEase(Ease.InBounce);
            clonePlayerBlock.transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360);
        }
        float randomX2, randomZ2;
        reSpawnAIBlocks = spawnCount - AIBlocks.Count;
        for (int i = 0; i < reSpawnAIBlocks; i++)
        {
            randomX2 = Random.Range(-9.5f, 9.5f);
            randomZ2 = Random.Range(-9.5f, 9.5f);

            Vector3 RandomPos2 = new Vector3(randomX2, 0.2f, randomZ2);
            GameObject cloneAIBlock = Instantiate(AIBlock, RandomPos2, Quaternion.identity);
            cloneAIBlock.transform.parent = transform;
            AIBlocks.Add(cloneAIBlock);
            cloneAIBlock.transform.localScale = new Vector3(0.4f, 0.1f, 0.15f);
            Vector3 scaleAI = new Vector3(
                cloneAIBlock.transform.localScale.x * 2f,
                cloneAIBlock.transform.localScale.y * 2f,
                cloneAIBlock.transform.localScale.z * 2f);
            cloneAIBlock.transform.DOScale(scaleAI, 1.5f).SetEase(Ease.InBounce);
            cloneAIBlock.transform.DORotate(new Vector3(0, -360, 0), 1, RotateMode.FastBeyond360);
        }
        StartCoroutine(SpawnBlock());
    }


    public void SpawnBlock(GameObject clonePlayerBlock,GameObject cloneAIBlock)
    {
        clonePlayerBlock.transform.parent = transform;
        playerBlocks.Add(clonePlayerBlock);
        clonePlayerBlock.transform.localScale = new Vector3(0.4f, 0.1f, 0.15f);
        Vector3 scalePlayer = new Vector3(clonePlayerBlock.transform.localScale.x * 2f, clonePlayerBlock.transform.localScale.y * 2f, clonePlayerBlock.transform.localScale.z * 2f);
        clonePlayerBlock.transform.DOScale(scalePlayer, 1.5f).SetEase(Ease.InBounce);
        clonePlayerBlock.transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360);


        cloneAIBlock.transform.parent = transform;
        AIBlocks.Add(cloneAIBlock);
        cloneAIBlock.transform.localScale = new Vector3(0.4f, 0.1f, 0.15f);
        Vector3 scaleAI = new Vector3(cloneAIBlock.transform.localScale.x * 2f, cloneAIBlock.transform.localScale.y * 2f, cloneAIBlock.transform.localScale.z * 2f);
        cloneAIBlock.transform.DOScale(scaleAI, 1.5f).SetEase(Ease.InBounce);
        cloneAIBlock.transform.DORotate(new Vector3(0, -360, 0), 1, RotateMode.FastBeyond360);

    }
}
