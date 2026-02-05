using UnityEngine;

public class GroundLooper : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform groundA;
    [SerializeField] private Transform groundB;

    [Header("Tuning")]
    [SerializeField] private float buffer = 2f;

    private float groundWidth;
    private Camera cam;

    void Start()
    {
        cam = target ? target.GetComponent<Camera>() : null;
        if (cam == null) cam = Camera.main;

        groundWidth = groundA.GetComponent<SpriteRenderer>().bounds.size.x;

        groundB.position = groundA.position + Vector3.right * groundWidth;
    }

    void Update()
    {
        float referenceX = (cam != null) ? cam.transform.position.x : target.position.x;

        float halfViewWidth = (cam != null) ? cam.orthographicSize * cam.aspect : 0f;
        float leftEdgeX = referenceX - halfViewWidth;

        float aRightEdge = groundA.position.x + groundWidth * 0.5f;

        if (aRightEdge < leftEdgeX - buffer)
        {
            groundA.position = groundB.position + Vector3.right * groundWidth;
            (groundA, groundB) = (groundB, groundA);
        }
    }
}




