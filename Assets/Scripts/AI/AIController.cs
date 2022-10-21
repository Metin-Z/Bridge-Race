using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AIController : MonoBehaviour
{
    public static AIController instance;
    public Transform BaseSlot;
    public float speed;
    Animator anim;
    public List<Transform> BlockList;
    public int randomBlock;
    public Vector3 target;
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
        anim = GetComponent<Animator>();
        //StartCoroutine(AIMovement());
    }
    private void Update()
    {
        if (!GameManager.instance.isGameRunning)
            return;
        //  randomBlock = Random.Range(0, GameManager.instance.AISpawnedBlocks.Count);
        //  target = GameManager.instance.AISpawnedBlocks[randomBlock].transform.position;
    }
    public IEnumerator AIMovement()
    {
        yield return new WaitForSeconds(2);
        if (GameManager.instance.AISpawnedBlocks !=null)
        {
            
            Sequence sequence1 = DOTween.Sequence();

            sequence1.Join(transform.DOMove(target, 4.5f).SetLoops(6));
  
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RedBlock")
        {
            other.transform.parent = BaseSlot;


            Vector3 targetLocalPosition = BaseSlot.localPosition;
            targetLocalPosition.y = BlockList.Count * 0.2F;

            GameObject block = other.gameObject;

            DOTween.Kill(block.GetInstanceID(), true);

            GameManager.instance.AIBlocks.Remove(block);
            block.transform.localEulerAngles = Vector3.zero;
            block.GetComponent<BoxCollider>().enabled = false;
            block.transform.DOLocalJump(targetLocalPosition, 1, 1, 0.25F);

            GameManager.instance.AISpawnedBlocks.Remove(block);
            other.GetComponent<BlockComponent>().AddList();
            BlockList.Add(block.transform);
        }
    }
    public void EndGameWin()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Dance", true);
        Quaternion target = Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 200);

    }
    public void EndGameLose()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Sad", true);
        Quaternion target = Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 200);

    }
}
