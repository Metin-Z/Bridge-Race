using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Linq;
public class AIController : MonoBehaviour
{
    public static AIController instance;
    public Transform BaseSlot;
    Animator _anim;
    NavMeshAgent _navmesh;
    public List<Transform> BlockList;
    public int randomBlock;
    public GameObject target;
    public GameObject brickTarget;
    public GameObject Finish;
    public bool place;

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
        _anim = GetComponent<Animator>();
        _navmesh = GetComponent<NavMeshAgent>();
        StartCoroutine(AIMovement());
    }
    private void Update()
    {
        if (!GameManager.instance.isGameRunning)
            return;

    }
    public GameObject myBridge1, mybridge2;
    public IEnumerator AIMovement()
    {
        while (!GameManager.instance.isGameRunning)
            yield return new WaitForSeconds(.1f);

        if (myBridge1.GetComponent<PlayerBridge1>().AIBricks1.Count == 0)
        {
            _navmesh.SetDestination(Finish.transform.position);
        }
        while (GameManager.instance.isGameRunning)
        {
            yield return new WaitForFixedUpdate();
            if (GameManager.instance.AISpawnedBlocks.Count > 0)
            {
                if (!target)
                {
                    _anim.SetBool("Run", true);
                    target = GameManager.instance.AISpawnedBlocks.Where(x => x.activeSelf).OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault().gameObject;
                }
                else if (target)
                {
                    if (transform.GetComponent<NavMeshAgent>().enabled == true)
                    {
                        if (BlockList.Count() < Random.Range(10, 19))
                        {
                            if (myBridge1.GetComponent<PlayerBridge1>().AIBricks1.Count > 0)
                            {
                                _navmesh.SetDestination(target.transform.position);
                                _anim.SetBool("Run", true);
                                int count = BlockList.Count();
                                yield return new WaitForSeconds(.15f * count);
                            }
                            
                        }
                        else
                        {
                            _navmesh.SetDestination(myBridge1.GetComponent<PlayerBridge1>().AIBricks1[0].transform.position);                           
                            int count = BlockList.Count();
                            yield return new WaitForSeconds(.15f * count);

                        }
                    }
                }
                if (BlockList.Count == 1)
                {
                    place = false;
                    transform.GetComponent<NavMeshAgent>().enabled = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RedBlock")
        {
            other.transform.parent = BaseSlot;

            target = null;
            Vector3 targetLocalPosition = BaseSlot.localPosition;
            targetLocalPosition.y = BlockList.Count * 0.2F;

            GameObject block = other.gameObject;

            DOTween.Kill(block.GetInstanceID(), true);

            GameManager.instance.AIBlocks.Remove(block);
            block.transform.localEulerAngles = Vector3.zero;
            block.GetComponent<BoxCollider>().enabled = false;
            block.transform.DOLocalJump(targetLocalPosition, 1, 1, 0.25F);

            GameManager.instance.AISpawnedBlocks.Remove(block);
            other.GetComponent<BlockComponent>().AddList(false);
            BlockList.Add(block.transform);
            _navmesh.speed = 5.5f;
        }
    }
    public void EndGameWin()
    {
        _anim.SetBool("Run", false);
        _anim.SetBool("Dance", true);
        Quaternion target = Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 200);

    }
    public void EndGameLose()
    {
        _anim.SetBool("Run", false);
        _anim.SetBool("Sad", true);
        Quaternion target = Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 200);

    }
    public IEnumerator GoBridge()
    {
        yield return new WaitForFixedUpdate();
        while (place == true)
        {
            Debug.Log("Block Koyuyor..");
            if (myBridge1.GetComponent<PlayerBridge1>().AIBricks1.Count > 0)
            {
                brickTarget = myBridge1.GetComponent<PlayerBridge1>().AIBricks1[0].gameObject;
                _navmesh.SetDestination(myBridge1.GetComponent<PlayerBridge1>().AIBricks1[0].transform.position);
            }
            yield return new WaitForSeconds(.5f);
        }
    }
    public void Go()
    {
        StartCoroutine(GoBridge());
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            int PlayerStackCount = PlayerController.instance.BlockList.Count;
            GameObject Player = PlayerController.instance.gameObject;
            for (int i = 0; i < PlayerStackCount; i++)
            {
                if (Player.GetComponent<PlayerController>().BlockList.LastOrDefault().gameObject.GetComponent<BlockComponent>())
                {
                    Player.GetComponent<PlayerController>().BlockList.LastOrDefault().gameObject.GetComponent<BlockComponent>().Fall();
                }
            }
            Debug.Log("AI Oyuncu'ya Çarptý");

        }
    }
}