using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform pointToFollow;

    private Vector2 rot;

    [SerializeField] private float speed;

    private void Start()
    {
        this.transform.position = pointToFollow.position;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        rot.x += mouse.y * speed * -1;
        rot.y += mouse.x * speed;

        rot.x = Mathf.Clamp(rot.x, -90f, 90f);

        this.transform.eulerAngles = new Vector3(rot.x, rot.y, 0);
        this.transform.position = this.pointToFollow.position;
    }
}
