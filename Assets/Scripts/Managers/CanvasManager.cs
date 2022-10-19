using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CanvasManager : MonoBehaviour
{ 
    public static CanvasManager instance;

    public TMP_Text name;
    public TMP_Text playerTXT;
    public TMP_Text AITXT;
    public GameObject StartPanel;
    public List<string> AINames;
    public string playerName;
   
    public virtual void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ReadPlayerName()
    {
        playerName = name.text;
        playerTXT.text = playerName;
        Debug.Log(playerName);
        StartPanel.SetActive(false);
        GameManager.instance.enabled = true;
        AIController.instance.enabled = true;
        PlayerController.instance.enabled = true;
        int AIRandom = Random.Range(0, 8);
        AITXT.text = AINames[AIRandom];
        GameManager.instance.isGameRunning = true;
    }
}
