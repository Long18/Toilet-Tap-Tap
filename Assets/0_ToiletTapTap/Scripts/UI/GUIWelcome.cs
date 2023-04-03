using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GUIWelcome : MonoBehaviour
{
    [Header("Listen Event")] [SerializeField]
    private BoolEventChannel onWelcomeEventChannel;

    [Header("Other")] [SerializeField] private GameObject panel;

    private void OnEnable()
    {
        onWelcomeEventChannel.OnEventRaised += OnActiveGUI;
    }

    private void OnDisable()
    {
        onWelcomeEventChannel.OnEventRaised -= OnActiveGUI;
    }

    private void OnActiveGUI(bool active)
    {
        panel.SetActive(active);
    }
}