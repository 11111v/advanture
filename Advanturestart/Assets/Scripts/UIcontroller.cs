using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{

    private Button StartButton;
    private Button SettingButton;
    private Button QuiteButton;
    private Button ModeButton;
    private Button ReturnSartPanelButton;
    private Button DetermineButton;
    public GameObject settingPanel;
    public AudioSource mixer;
    public Slider slider;

    public Dropdown dropdown;
    private void Start()
    {
        StartButton = GameObject.Find("UIRoot/StartView/startButton").GetComponent<Button>();         
        StartButton.onClick.AddListener(StartGame);



        SettingButton = GameObject.Find("UIRoot/StartView/settingButton").GetComponent<Button>();
        SettingButton.onClick.AddListener(OpenSettingPanel);


        QuiteButton = GameObject.Find("UIRoot/StartView/quiteButton").GetComponent<Button>();
        QuiteButton.onClick.AddListener(QuiteGame);

        ReturnSartPanelButton = settingPanel.transform.Find("ReturnButton").GetComponent<Button>();
        ReturnSartPanelButton.onClick.AddListener(ReturnStartPanel);


        DetermineButton= settingPanel.transform.Find("determineButton").GetComponent<Button>();
        DetermineButton.onClick.AddListener(DropdownChange);
    }
    //开始游戏加载第一关
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    //打开设置面板
    private void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    //返回开始面板
    private void ReturnStartPanel()
    {
        settingPanel.SetActive(false);
    }

    //退出游戏
    private void QuiteGame()
    {
        Application.Quit();
    }

    //分辨率选择
    private void DropdownChange()
    {
        switch (dropdown.value)
        {
            case 0:
                {
                    Screen.SetResolution(1920, 1080, true);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1440, 900, false);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(1280, 720, false);
                    break;
                }
            default:
                break;
        }
    }
    private void Update()
    {
        mixer.volume = slider.value;
    }
}
