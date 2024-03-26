using System;
using System.Collections;
using System.Collections.Generic;
using Keyboard;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private Typing typing;

    [SerializeField]
    private TextMeshProUGUI wpmCounter;
    
    [SerializeField]
    private TextMeshProUGUI accuracyCounter;
    // Start is called before the first frame update
    void Awake()
    {
        typing = gameObject.GetComponent<Typing>();
        typing.StartTimingOnFirstKeyPress();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (typing.GetAccuracy() >= 1 && typing.GetTotalCharacters() == typing.GetTotalTypedCharacters() && typing.IsRunning)
        {
            typing.StopTiming();
        }
        wpmCounter.SetText($"Words per minute: {Math.Round(typing.GetRawWpm())}");
        accuracyCounter.SetText($"Accuracy: {typing.GetAccuracy():P2}");
    }
    
}
