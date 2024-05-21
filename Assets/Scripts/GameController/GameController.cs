using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player Player => player;

    [SerializeField]
    private Player player;
    
    [SerializeField]
    private Transform followObj;
    private GameObject cam;
    
    [SerializeField]
    private GameObject pCam;
    
    // Start is called before the first frame update
    void Start()
    {
        // Camera Initialization
        if (followObj == null) followObj = new GameObject("Camera Follow Object").transform;
        cam = Instantiate(pCam);
        CinemachineVirtualCamera vcam = cam.GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = followObj;
        vcam.LookAt = followObj;

        player.movementScript.cam = cam;
    }
}
