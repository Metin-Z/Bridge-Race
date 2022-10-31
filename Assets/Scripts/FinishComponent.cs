using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinishComponent : MonoBehaviour
{
    public Transform WinnerPos, LoserPos;
    public List<GameObject> Players;
    public GameObject FinishCam;
    public GameObject Conf;
    public Transform ConfPos;
    void Start()
    {
        Players.Add(PlayerController.instance.gameObject);
        Players.Add(AIController.instance.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            transform.GetComponent<AudioSource>().enabled = true;
            GameManager.instance.isGameRunning = false;
            Players.Remove(other.gameObject);
            Players[0].transform.position = LoserPos.position;
            FinishCam.SetActive(true);
            Instantiate(Conf, ConfPos.transform.position, Quaternion.EulerRotation(120,0,0));
            CameraComponent.instance.gameObject.SetActive(false);

            if (other.GetComponent<PlayerController>())
            {
                other.GetComponent<PlayerController>().EndGameWin();
                other.transform.position = WinnerPos.position;
                Players[0].GetComponent<AIController>().EndGameLose();
                AIController.instance.place = false;
            }
            if (other.GetComponent<AIController>())
            {
                other.GetComponent<AIController>().EndGameWin();
                other.transform.position = WinnerPos.position;
                other.GetComponent<NavMeshAgent>().enabled = false;
                Players[0].GetComponent<PlayerController>().EndGameLose();
                AIController.instance.place = false;
            }
            CanvasManager.instance.Finish();   
            
        }
    }
}
