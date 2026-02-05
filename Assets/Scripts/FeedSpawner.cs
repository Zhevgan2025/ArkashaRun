using UnityEngine;

public class FeedSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform target;
    [SerializeField] private GameObject feedPrefab;

    [Header("Spawn timing")]
    [SerializeField] private float spawnAhead = 14f;
    [SerializeField] private float minDelay = 1.8f;
    [SerializeField] private float maxDelay = 3.2f;

    [Header("Ground snap")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayStartY = 50f;
    [SerializeField] private float rayDistance = 200f;
    [SerializeField] private float yExtra = 0.02f;

    [Header("No spawn on obstacles")]
    [SerializeField] private LayerMask blockedLayer;
    [SerializeField] private float checkRadius = 0.35f;

    private float nextTime;

    private void Start() => ScheduleNext();

    private void Update()
    {
        if (Time.time < nextTime) return;
        if (!target || !feedPrefab) { ScheduleNext(); return; }

        float x = target.position.x + spawnAhead;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, rayStartY), Vector2.down, rayDistance, groundLayer);

        if (hit.collider != null)
        {
            float halfH = 0.5f;
            Collider2D col = feedPrefab.GetComponent<Collider2D>();
            if (col != null) halfH = col.bounds.extents.y;

            Vector2 pos = new Vector2(x, hit.point.y + halfH + yExtra);

            bool blocked = Physics2D.OverlapCircle(pos, checkRadius, blockedLayer) != null;

            if (!blocked)
            {
                Instantiate(feedPrefab, pos, Quaternion.identity);
            }
        }

        ScheduleNext();
    }

    private void ScheduleNext()
    {
        nextTime = Time.time + Random.Range(minDelay, maxDelay);
    }
}

