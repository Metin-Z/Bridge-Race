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
        xMinClamp = -9.5f;
        xMaxClamp = 9.5f;

        zMinClamp = -9.5f;
        zMaxClamp = 9.5f;
    }
    private void Update()
    {
        Clamp(xMinClamp, xMaxClamp, zMinClamp, zMaxClamp);
    }
    public void Clamp(float xMinClamp , float xMaxClamp,float zMinClamp, float zMaxClamp)
    {
        float xPos = Mathf.Clamp(transform.position.x, xMinClamp, xMaxClamp);
        float zPos = Mathf.Clamp(transform.position.z, zMinClamp, zMaxClamp);
        transform.position = new Vector3(xPos, transform.position.y, zPos);
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
        if (other.gameObject.tag =="BlueBlock")
        {
            other.transform.parent = transform;
            Vector3 lastElement = new Vector3(
                BlockList.FirstOrDefault().transform.localPosition.x,
                BlockList.LastOrDefault().transform.localPosition.y+.2f,
                BlockList.FirstOrDefault().transform.localPosition.z);        
            GameObject block = other.gameObject;
            GameManager.instance.playerBlocks.Remove(block);
            block.transform.localEulerAngles = Vector3.zero;
            BlockList.Add(block.transform);
            block.GetComponent<BoxCollider>().enabled = false;
            block.transform.DOLocalJump(lastElement,1,1,1);
        }
    }
}
