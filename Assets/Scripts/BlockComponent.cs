using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class BlockComponent : MonoBehaviour
{
    public Vector3 spawnPos;
    public Vector3 startScale;
    Vector3 targetScale;

    Rigidbody rb;

    public bool fallControl;

    public Color blueBlock, redBlock;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetScale = transform.localScale;
        transform.localScale = startScale;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(transform.DOScale(targetScale, 1.5f).SetEase(Ease.InBounce));
        sequence.Join(transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear));

        sequence.SetId(gameObject.GetInstanceID());
        sequence.Play();

        spawnPos = transform.position;
        transform.SetParent(GameManager.instance.transform);
    }
    public void AddList(bool isPlayer)
    {
        if (fallControl == false)
        {
            if (isPlayer)
            {

                if (transform.GetComponent<BoxCollider>().enabled == false)
                {
                    GameManager.instance.blockPosForPlayer.Add(spawnPos);
                }
            }
            else
            {
                if (transform.GetComponent<BoxCollider>().enabled == false)
                {
                    GameManager.instance.blockPosForAi.Add(spawnPos);
                }
            }
        }
    }


    public void Fall()
    {
        fallControl = true;
        transform.parent = null;
        transform.GetComponent<BoxCollider>().enabled = true;
        transform.GetComponent<BoxCollider>().isTrigger = false;
        rb.isKinematic = false;
        int randomForceBack = Random.Range(150, 300);
        int randomForceRight = Random.Range(300, 600);
        int randomForceLeft = Random.Range(150, 300);
        rb.AddForce(-transform.forward * randomForceBack);
        rb.AddForce(transform.right * randomForceRight);
        rb.AddForce(-transform.right * randomForceLeft);
        if (transform.gameObject.CompareTag("BlueBlock"))
        {        
            PlayerController.instance.BlockList.Remove(transform);
            transform.GetComponent<MeshRenderer>().material.DOColor(redBlock, 0.75f);
            gameObject.tag = "RedBlock";
            GameManager.instance.AISpawnedBlocks.Add(transform.gameObject);
            return;
        }
        if (transform.gameObject.CompareTag("RedBlock"))
        {
            AIController.instance.BlockList.Remove(transform);
            transform.GetComponent<MeshRenderer>().material.DOColor(blueBlock, 0.75f);
            gameObject.tag = "BlueBlock";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.isKinematic = true;
            transform.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
