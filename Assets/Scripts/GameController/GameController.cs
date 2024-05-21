using Cinemachine;
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
    }
}
