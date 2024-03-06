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
    /// The speed the Camera moves to the target.
    /// </summary>
    public float speed = 1.0f;
    
    /// <summary>
    /// The offset from the target for the Camera to go to.
    /// </summary>
    private Vector3 offset;
    
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
    }
    
    /// <summary>
    /// Changes the who the Camera is currently following.
    /// </summary>
    /// <param name="target">The target for the Camera to follow.</param>
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    /// <summary>
    /// Changes the offset from the target for the Camera to follow.
    /// </summary>
    /// <param name="offset">The new offset.</param>
    public void SetOffset(Vector3 offset)
    {
        this.offset = offset;
    }
    
    public void LateUpdate()
    {
        Vector3 pos = this.transform.position;
        Vector3 targetPos = target.transform.position;
        Vector3 moveTo = new Vector3(
            Mathf.Lerp(pos.x, targetPos.x + offset.x, speed), 
            Mathf.Lerp(pos.y, targetPos.y + offset.y, speed), 
            Mathf.Lerp(pos.z, targetPos.z + offset.z, speed)
        );
        this.transform.position = moveTo;
    }
}