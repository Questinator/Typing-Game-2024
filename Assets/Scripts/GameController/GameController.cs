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
}
