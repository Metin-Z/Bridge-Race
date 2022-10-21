using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishComponent : MonoBehaviour
{
    public Transform WinnerPos, LoserPos;
    public List<GameObject> Players;
    void Start()
    {
        Players.Add(PlayerController.instance.gameObject);
        Players.Add(AIController.instance.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            GameManager.instance.isGameRunning = false;
            Players.Remove(other.gameObject);
            other.transform.position = WinnerPos.position;
            Players[0].transform.position = LoserPos.position;

            if (other.GetComponent<PlayerController>())
            {
                other.GetComponent<PlayerController>().EndGameWin();
                Players[0].GetComponent<AIController>().EndGameLose();
            }
            if (other.GetComponent<AIController>())
            {
                other.GetComponent<AIController>().EndGameWin();
                Players[0].GetComponent<PlayerController>().EndGameLose();
            }
            
        }
    }
}
