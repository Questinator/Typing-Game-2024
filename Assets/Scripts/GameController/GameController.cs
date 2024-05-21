using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player Player => player;
    public UIController UIController => uIController;

    [SerializeField]
    private Player player;

    [SerializeField]
    private UIController uIController; // this naming is cursed but I don't care
    
    [SerializeField]
    private Transform followObj;
    private GameObject cam;
    
    // Start is called before the first frame update
    void Start()
    {
        // Camera Initialization
        if (followObj == null) followObj = new GameObject("Camera Follow Object").transform;
        cam = new GameObject("Main Camera");
        cam.AddComponent<Camera>();
        cam.AddComponent<AudioListener>();
        cam.AddComponent<CameraController>().Init(followObj);
    }
}
