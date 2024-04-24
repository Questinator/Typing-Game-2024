using UnityEngine;
public class CameraController : MonoBehaviour
{
    
    /// <summary>
    /// The target that the Camera follows.
    /// </summary>
    private Transform target;

    /// <summary>
    /// The speed the Camera moves to the target.
    /// </summary>
    public float speed = 10f;
    
    /// <summary>
    /// The offset from the target for the Camera to go to.
    /// </summary>
    private Vector3 offset;
    
    /// <summary>
    /// Controls everything to do with the Camera.
    /// </summary>
    /// <param name="target">The <see cref="GameObject"/> for the Camera to follow.</param>
    /// <param name="speed">The speed that this Camera follows at.</param>
    public void Init(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }
    
    /// <summary>
    /// Controls everything to do with the Camera.
    /// </summary>
    /// <param name="target">The <see cref="GameObject"/> for the Camera to follow.</param>
    public void Init(Transform target)
    {
        this.target = target;
    }
    
    /// <summary>
    /// Changes the what this Camera is currently following.
    /// </summary>
    /// <param name="target">The <see cref="GameObject"/> for this Camera to follow.</param>
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    /// <summary>
    /// Changes the offset away from <see cref="target"/> that this Camera follows at.
    /// </summary>
    /// <param name="offset">The new offset.</param>
    public void SetOffset(Vector3 offset)
    {
        this.offset = offset;
    }
    
    /// <summary>
    /// Changes the speed that this Camera follows <see cref="target"/> at.
    /// </summary>
    /// <param name="speed">The new speed value.</param>
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    
    public void LateUpdate()
    {
        Vector3 pos = this.transform.position;
        Vector3 targetPos = target.position;
        Vector3 moveTo = new Vector3(
            Mathf.Lerp(pos.x, targetPos.x + offset.x, speed * Time.deltaTime), 
            Mathf.Lerp(pos.y, targetPos.y + offset.y, speed * Time.deltaTime), 
            Mathf.Lerp(pos.z, targetPos.z + offset.z, speed * Time.deltaTime)
        );
        this.transform.position = moveTo;
    }
}