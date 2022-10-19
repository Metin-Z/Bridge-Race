using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrickComponent : MonoBehaviour
{
    public bool player, AI;

    public PlayerBridge1 bridge;
    private void Start()
    {
        GameObject playerBridge = transform.parent.root.gameObject;
        bridge = playerBridge.GetComponent<PlayerBridge1>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (player == true && collision.gameObject.tag == "Player" && 
            PlayerController.instance.BlockList.Count > 1 && 
            bridge.PlayerBricks1.Count > 0)
        {
            Destroy(transform.GetComponent<BrickComponent>());
            Destroy(PlayerController.instance.BlockList.LastOrDefault().gameObject);
            PlayerController.instance.BlockList.Remove(PlayerController.instance.BlockList.LastOrDefault());
            bridge.PlayerBricks1[0].SetActive(true);
            bridge.PlayerBricks1.RemoveAt(0);
        }

        if (AI == true && collision.gameObject.tag == "AI" &&
            AIController.instance.BlockList.Count > 1 &&
            bridge.AIBricks1.Count > 0)
        {
            Debug.Log("adsadsad0");
            Destroy(transform.GetComponent<BrickComponent>());
            Destroy(AIController.instance.BlockList.LastOrDefault().gameObject);
            AIController.instance.BlockList.Remove(AIController.instance.BlockList.LastOrDefault());
            bridge.AIBricks1[0].SetActive(true);
            bridge.AIBricks1.RemoveAt(0);
        }
    }
}
