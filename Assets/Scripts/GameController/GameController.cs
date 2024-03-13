using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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

    /// <summary>
    /// Switches to the right scene to start the game
    /// </summary>
    public void PlayGame()
    {
        Debug.Log("Playing the game!");
        // TODO: Write code that will start the right scene
    }
}
