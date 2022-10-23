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
    public float speed;
    Animator _anim;
    NavMeshAgent _navmesh;
    public List<Transform> BlockList;
    public int randomBlock;
    public GameObject target;
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
        DontDestroyOnLoad(gameObject);
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




        while (GameManager.instance.isGameRunning)
        {
            yield return new WaitForFixedUpdate();
            if (GameManager.instance.AISpawnedBlocks.Count > 0)
            {
                if (!target)
                {
                    Debug.Log("if'in içine girdi");
                    _anim.SetBool("Run", true);
                    target = GameManager.instance.AISpawnedBlocks.Where(x => x.activeSelf).OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault().gameObject;
                }
                else if (target)
                {
                    if (transform.GetComponent<NavMeshAgent>().enabled == true)
                    {
                        if (BlockList.Count() < Random.Range(10, 19))
                        {
                            _navmesh.SetDestination(target.transform.position);
                            _anim.SetBool("Run", true);
                            int count = BlockList.Count();
                            yield return new WaitForSeconds(.15f * count);
                        }
                        else
                        {

                            _navmesh.SetDestination(myBridge1.GetComponent<PlayerBridge1>().AIBricks1[0].transform.position);
                            int count = BlockList.Count();
                            yield return new WaitForSeconds(.15f * count);

                        }
                    }
                    else
                    {
                        if (BlockList.Count ==1)
                        {
                            place = false;
                            transform.GetComponent<NavMeshAgent>().enabled = true;
                        }
                    }
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
        while (place == true)
            yield return new WaitForSeconds(.1f);
        yield return new WaitForFixedUpdate();
        Debug.Log("Block Koyuyor..");
        transform.GetComponent<NavMeshAgent>().enabled = false;
        transform.DOMove(myBridge1.GetComponent<PlayerBridge1>().AIBricks1[0].transform.position, 3).OnComplete(() => Go());
        //transform.Translate(3 * Time.fixedDeltaTime * myBridge1.GetComponent<PlayerBridge1>().AIBricks1[0].transform.position, Space.World);
    }
    public void Go()
    {
        StartCoroutine(GoBridge());
    }
}