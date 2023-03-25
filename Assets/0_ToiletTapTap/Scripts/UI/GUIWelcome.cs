using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIWelcome : MonoBehaviour
{
    [Header("Listen Event")] [SerializeField]
    private BoolEventChannel onReadyEventChannel;

    [Header("Other")] [SerializeField] private GameObject panel;

    private void OnEnable()
    {
        onReadyEventChannel.OnEventRaised += OnActiveGUI;
    }

    private void OnDisable()
    {
        onReadyEventChannel.OnEventRaised -= OnActiveGUI;
    }

    private void OnActiveGUI(bool active)
    {
        panel.SetActive(active);
    }
}