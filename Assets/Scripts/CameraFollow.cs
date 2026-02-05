using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool followX = true;
    [SerializeField] private float xOffset = 4f;
    [SerializeField] private float fixedY = 2f;
    [SerializeField] private float smoothTime = 0.12f;

    private Vector3 vel;

    private void Awake()
    {
        Snap();
    }

    private void OnEnable()
    {
        Snap();
    }

    private void Snap()
    {
        if (!target) return;

        float x = followX ? target.position.x + xOffset : transform.position.x; 
        transform.position = new Vector3(x, fixedY, transform.position.z);
    }

    private void LateUpdate()
    {
        if (!target) return;

        float x = followX ? target.position.x + xOffset : transform.position.x; 
        Vector3 desired = new Vector3(x, fixedY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref vel, smoothTime);
    }
}






