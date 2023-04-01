using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    private Button StartButton;
    public GameObject StartPanel;
    private void Start()
    {
        StartButton = transform.Find("Canvas/startPanel/startButton").GetComponent<Button>();
        //StartPanel = GameObject.Find("Canvas/startPanel");
        StartButton.onClick.AddListener(startGame);
    }
    private void startGame()
    {
        StartPanel.SetActive(false);
    }
    private void Update()
    {

    }
}
