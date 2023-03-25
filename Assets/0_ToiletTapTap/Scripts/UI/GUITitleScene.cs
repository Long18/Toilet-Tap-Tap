using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITitleScene : MonoBehaviour
{
    [SerializeField] private BoolEventChannel onWelcomeEventChannel;
    [SerializeField] private LoadEventChannelSO loadSceneEventChannel;
    [SerializeField] private GameSceneSO gameplaySceneSO;

    private void OnEnable()
    {
        onWelcomeEventChannel.RaiseEvent(true); 
    }
    
    public void OnGoToMainScene()
    {
        loadSceneEventChannel.RequestLoadScene(gameplaySceneSO);
    }
    
    private void OnDisable()
    {
        // onWelcomeEventChannel.RaiseEvent(false);
    }
}