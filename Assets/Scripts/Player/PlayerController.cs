using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Transform BaseSlot;
    public DynamicJoystick dynamicJoystick;
    public float speed;
    Animator anim;
    public List<Transform> BlockList;

    public float xMinClamp, xMaxClamp, zMinClamp, zMaxClamp;
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
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isGameRunning)
            return;
        JoyStickMovement();
    }
    public void JoyStickMovement()
    {
        Vector3 moveForward = new Vector3(dynamicJoystick.Horizontal, 0, dynamicJoystick.Vertical);
        transform.Translate(speed * Time.fixedDeltaTime * moveForward, Space.World);
        moveForward.Normalize();
        if (dynamicJoystick.Horizontal !=0 || dynamicJoystick.Vertical !=0)
        {
            transform.forward = moveForward;
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlueBlock")
        {
            other.transform.parent = BaseSlot;

            Vector3 targetLocalPosition = BaseSlot.localPosition;
            targetLocalPosition.y = BlockList.Count * 0.2F;

            GameObject block = other.gameObject;

            DOTween.Kill(block.GetInstanceID(), true);

            GameManager.instance.playerBlocks.Remove(block);
            block.transform.localEulerAngles = Vector3.zero;
            block.GetComponent<BoxCollider>().enabled = false;
            block.transform.DOLocalJump(targetLocalPosition, 1, 1,0.25F);

            other.GetComponent<BlockComponent>().AddList();
            BlockList.Add(block.transform);
        }
    }
}
