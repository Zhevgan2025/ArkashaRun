using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField, Range(0f, 1f)] private float parallax = 0.5f;
    [SerializeField] private bool lockY = true;

    private Vector3 startPos;
    private float startCamX;
    private float startCamY;

    private void Awake()
    {
        if (!cam) cam = Camera.main.transform;
        startPos = transform.position;
        startCamX = cam.position.x;
        startCamY = cam.position.y;
    }

    private void LateUpdate()
    {
        if (!cam) return;

        float dx = (cam.position.x - startCamX) * parallax;
        float dy = (cam.position.y - startCamY) * parallax;

        float y = lockY ? startPos.y : startPos.y + dy;
        transform.position = new Vector3(startPos.x + dx, y, startPos.z);
    }
}
