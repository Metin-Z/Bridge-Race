using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    public float speed;
    Animator anim;
    public List<Transform> BlockList;

    public float xMinClamp, xMaxClamp, zMinClamp, zMaxClamp;
    private void Start()
    {
        anim = GetComponent<Animator>();      
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
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
            other.transform.parent = transform;
            Vector3 lastElement = new Vector3(
                BlockList.FirstOrDefault().transform.localPosition.x,
                BlockList.LastOrDefault().transform.localPosition.y + .2f,
                BlockList.FirstOrDefault().transform.localPosition.z);
            GameObject block = other.gameObject;
            GameManager.instance.playerBlocks.Remove(block);
            block.transform.localEulerAngles = Vector3.zero;
            BlockList.Add(block.transform);
            block.GetComponent<BoxCollider>().enabled = false;
            block.transform.DOLocalJump(lastElement, 1, 1, 1);
            other.GetComponent<BlockComponent>().AddList();
        }
    }
}
