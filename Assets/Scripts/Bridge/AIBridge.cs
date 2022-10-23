using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class AIBridge : MonoBehaviour
{
    public GameObject bridge;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AIController>())
        {
            if (AIController.instance.BlockList.Count > 1)
            {
                other.GetComponent<AIController>().Go();
                AIController.instance.place = true;
            }
            
        }
    }
}