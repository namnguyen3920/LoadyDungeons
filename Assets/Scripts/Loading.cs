﻿using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;


public class Loading : MonoBehaviour
{
    private static AsyncOperationHandle<SceneInstance> m_SceneLoadingOpHandle;

    private AsyncOperation m_SceneOperation;

    [SerializeField]
    private Slider m_LoadingSlider;

    [SerializeField]
    private GameObject m_PlayButton, m_LoadingText;

    private void Awake()
    {
        StartCoroutine(loadNextLevel("Level_0" + GameManager.s_CurrentLevel));
    }

    private IEnumerator loadNextLevel(string level)
    {

        m_SceneLoadingOpHandle = Addressables.LoadSceneAsync(level, activateOnLoad: true);

        //m_SceneOperation = SceneManager.LoadSceneAsync(level);
        //m_SceneOperation.allowSceneActivation = false;

        while (!m_SceneLoadingOpHandle.IsDone)
        {
            //m_LoadingSlider.value = m_SceneOperation.progress;
            m_LoadingSlider.value = m_SceneLoadingOpHandle.PercentComplete;

            if (m_SceneLoadingOpHandle.PercentComplete >= 0.9f && !m_PlayButton.activeInHierarchy)
                m_PlayButton.SetActive(true);

            yield return null;
        }

        Debug.Log($"Loaded Level {level}");
    }

    // Function to handle which level is loaded next
    //public void GoToNextLevel()
    //{
    //    m_SceneOperation.allowSceneActivation = true;
    //}
}
