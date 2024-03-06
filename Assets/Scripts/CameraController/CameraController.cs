using System;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// The actual Camera object.
    /// </summary>
    private GameObject cam;
    
    /// <summary>
    /// The target that the Camera follows.
    /// </summary>
    private GameObject target;
    
    /// <summary>
    /// The speed the camera moves to the target.
    /// </summary>
    private float speed;
    
    /// <summary>
    /// Controls everything to do with the Camera.
    /// </summary>
    /// <param name="target">The target for the Camera to follow.</param>
    public CameraController(GameObject target)
    {
        cam = new GameObject("Main Camera");
        cam.AddComponent<Camera>();
        cam.AddComponent<AudioListener>();

        this.target = target;

        speed = 1.0f;
    }
    
    /// <summary>
    /// Changes the who the Camera is currently following.
    /// </summary>
    /// <param name="target">The target for the Camera to follow.</param>
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    
    public void LateUpdate()
    {
        Vector3 pos = this.transform.position;
        Vector3 moveTo = new Vector3(Mathf.Lerp(pos.x, pos.x, speed), Mathf.Lerp(pos.y, pos.y, speed), Mathf.Lerp(pos.z, pos.z, speed));
        this.transform.position = moveTo;
    }
}