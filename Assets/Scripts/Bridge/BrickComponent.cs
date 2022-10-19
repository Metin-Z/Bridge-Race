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
        if (player == true && collision.gameObject.tag == "Player"&&PlayerController.instance.BlockList.Count>1)
        {
            Destroy(transform.GetComponent<BrickComponent>());
            Destroy(PlayerController.instance.BlockList.LastOrDefault().gameObject);
            PlayerController.instance.BlockList.Remove(PlayerController.instance.BlockList.LastOrDefault());
            bridge.PlayerBricks1[0].SetActive(true);
            bridge.PlayerBricks1.RemoveAt(0);
        }
    }
}
