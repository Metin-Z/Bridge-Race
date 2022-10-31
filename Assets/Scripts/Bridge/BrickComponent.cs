using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrickComponent : MonoBehaviour
{
    public bool player, AI;

    public PlayerBridge1 bridge;
    public GameObject playerBridge;
    private void Start()
    {
        bridge = playerBridge.GetComponent<PlayerBridge1>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (bridge != null)
        {
            if (player == true && collision.gameObject.tag == "Player" &&
                PlayerController.instance.BlockList.Count > 1 &&
                bridge.PlayerBricks1.Count > 0)
            {
                GameManager.instance.BrickSound();
                Destroy(transform.GetComponent<BrickComponent>());
                Destroy(PlayerController.instance.BlockList.LastOrDefault().gameObject);
                PlayerController.instance.BlockList.Remove(PlayerController.instance.BlockList.LastOrDefault());
                bridge.PlayerBricks1[0].SetActive(true);
                bridge.PlayerBricks1.RemoveAt(0);
            }

           
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (AI == true && collision.gameObject.tag == "AI" &&
               AIController.instance.BlockList.Count > 1 &&
               bridge.AIBricks1.Count > 0)
        {
            Destroy(transform.GetComponent<BrickComponent>());
            Destroy(AIController.instance.BlockList.LastOrDefault().gameObject);
            AIController.instance.BlockList.Remove(AIController.instance.BlockList.LastOrDefault());
            AIController.instance.place = true;
            collision.transform.GetComponent<AIController>().Go();
            bridge.AIBricks1[0].SetActive(true);
            bridge.AIBricks1.RemoveAt(0);
        }
    }
}
