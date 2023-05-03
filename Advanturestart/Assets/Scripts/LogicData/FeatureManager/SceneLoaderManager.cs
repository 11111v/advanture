using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{


    public static SceneLoaderManager Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }
    //public GameObject loadingUI; // 加载进度 UI
    //public float delayTime = 0.5f; // 加载进度 UI 延迟显示时间

    private AsyncOperation asyncLoad; // 异步加载场景的操作对象

    // 加载场景
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    // 加载场景
    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneAsync(index));
    }
    // 异步加载场景并显示加载进度 UI
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // 显示加载进度 UI
        //yield return new WaitForSeconds(delayTime);
        //loadingUI.SetActive(true);

        // 异步加载场景
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // 不自动激活场景

        // 更新加载进度 UI
        while (asyncLoad.progress < 0.9f)
        {
            //loadingUI.GetComponent<LoadingUI>().UpdateProgress(asyncLoad.progress);
            yield return null;
        }

        //loadingUI.GetComponent<LoadingUI>().UpdateProgress(1.0f);

        // 等待玩家按下按钮来激活场景
        while (!asyncLoad.isDone)
        {
            //if (Input.GetButtonDown("Jump")) // 假设按下 Jump 键可以激活场景
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        // 隐藏加载进度 UI
        //loadingUI.SetActive(false);
    }
    // 异步加载场景并显示加载进度 UI
    private IEnumerator LoadSceneAsync(int index)
    {
        // 显示加载进度 UI
        //yield return new WaitForSeconds(delayTime);
        //loadingUI.SetActive(true);

        // 异步加载场景
        asyncLoad = SceneManager.LoadSceneAsync(index);
        asyncLoad.allowSceneActivation = false; // 不自动激活场景

        // 更新加载进度 UI
        while (asyncLoad.progress < 0.9f)
        {
            //loadingUI.GetComponent<LoadingUI>().UpdateProgress(asyncLoad.progress);
            yield return null;
        }

        //loadingUI.GetComponent<LoadingUI>().UpdateProgress(1.0f);

        // 等待玩家按下按钮来激活场景
        while (!asyncLoad.isDone)
        {
            //if (Input.GetButtonDown("Jump")) // 假设按下 Jump 键可以激活场景
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        // 隐藏加载进度 UI
        //loadingUI.SetActive(false);
    }
    // 卸载场景
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}