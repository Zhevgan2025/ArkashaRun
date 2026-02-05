using UnityEngine;

public class InfiniteParallaxLoop : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform bg1;
    [SerializeField] private Transform bg2;

    [Header("Parallax")]
    [Range(0f, 1f)]
    [SerializeField] private float parallax = 0.2f;
    [SerializeField] private bool lockY = true;

    [Header("Seam Fix")]
    [Tooltip("Small overlap to hide seams (world units). Try 0.02 - 0.1")]
    [SerializeField] private float overlap = 0.05f;

    [Tooltip("Must match your texture Pixels Per Unit (often 100)")]
    [SerializeField] private float pixelsPerUnit = 100f;

    private float width;
    private Vector3 camPrev;

    private void Awake()
    {
        if (!cam) cam = Camera.main.transform;

        var sr = bg1.GetComponent<SpriteRenderer>();
        width = sr.bounds.size.x;

        AlignSecondToFirst();

        camPrev = cam.position;
    }

    private void LateUpdate()
    {
        if (!cam || !bg1 || !bg2) return;

        float camDeltaX = cam.position.x - camPrev.x;

        MoveParallax(bg1, camDeltaX);
        MoveParallax(bg2, camDeltaX);

        Loop();

        camPrev = cam.position;
    }

    private void MoveParallax(Transform t, float camDeltaX)
    {
        Vector3 p = t.position;
        p.x += camDeltaX * parallax;

        if (lockY) p.y = t.position.y;

        p.x = Mathf.Round(p.x * pixelsPerUnit) / pixelsPerUnit;

        t.position = p;
    }

    private void Loop()
    {
        Transform left = (bg1.position.x < bg2.position.x) ? bg1 : bg2;
        Transform right = (left == bg1) ? bg2 : bg1;

        if (cam.position.x - left.position.x > width)
        {
            Vector3 p = left.position;
            p.x = right.position.x + width - overlap;
            p.x = Mathf.Round(p.x * pixelsPerUnit) / pixelsPerUnit;
            left.position = p;
        }
    }

    private void AlignSecondToFirst()
    {
        Vector3 p = bg2.position;
        p.x = bg1.position.x + width - overlap;
        bg2.position = p;
    }
}


