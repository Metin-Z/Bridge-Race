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
    void Start()
    {
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
