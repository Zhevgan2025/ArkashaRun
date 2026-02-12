using UnityEngine;

public class CoinSpeedSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform target;
    [SerializeField] private GameObject coinSpeedPrefab;

    [Header("Spawn timing")]
    [SerializeField] private float spawnAhead = 16f;
    [SerializeField] private float minDelay = 14f;
    [SerializeField] private float maxDelay = 24f;

    [Header("Chance")]
    [Range(0f, 1f)]
    [SerializeField] private float spawnChance = 0.45f;

    [Header("Ground snap")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayStartY = 50f;
    [SerializeField] private float rayDistance = 200f;
    [SerializeField] private float yExtra = 0.02f;

    [Header("No spawn on obstacles")]
    [SerializeField] private LayerMask blockedLayer;
    [SerializeField] private float checkRadius = 0.45f;

    private float nextTime;

    private void Start() => ScheduleNext();

    private void Update()
    {
        if (Time.time < nextTime) return;
        ScheduleNext();

        if (!target || !coinSpeedPrefab) return;

        if (Random.value > spawnChance) return;

        float x = target.position.x + spawnAhead;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, rayStartY), Vector2.down, rayDistance, groundLayer);
        if (hit.collider == null) return;

        float halfH = 0.5f;
        Collider2D col = coinSpeedPrefab.GetComponent<Collider2D>();
        if (col != null) halfH = col.bounds.extents.y;

        Vector2 pos = new Vector2(x, hit.point.y + halfH + yExtra);

        bool blocked = Physics2D.OverlapCircle(pos, checkRadius, blockedLayer) != null;
        if (blocked) return;

        Instantiate(coinSpeedPrefab, pos, Quaternion.identity);
    }

    private void ScheduleNext()
    {
        nextTime = Time.time + Random.Range(minDelay, maxDelay);
    }
}

