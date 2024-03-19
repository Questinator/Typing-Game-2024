using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform followObj;
    private GameObject cam;

    private ItemController itemController;

    internal static GameController controllerReference;
    public static GameController GlobalController
    {
        get
        {
            if (controllerReference == null)
            {
                controllerReference = new GameController();
            }

            return controllerReference;
        }
    }

    void Start()
    {
        // Camera Initialization
        if (followObj == null) followObj = new GameObject("Camera Follow Object").transform;
        cam = new GameObject("Main Camera");
        cam.AddComponent<Camera>();
        cam.AddComponent<AudioListener>();
        cam.AddComponent<CameraController>().Init(followObj);
        
        // Item Initialization
        itemController = new ItemController();
    }

    public ItemController GetItemController()
    {
        return itemController;
    }
}
