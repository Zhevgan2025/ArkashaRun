using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform target;

    [Header("Prefabs")]
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject lowBarrierPrefab;

    [Header("Roof Segment")]
    [SerializeField] private GameObject roofPrefab;
    [Range(0f, 1f)][SerializeField] private float roofChance = 0.12f;

    [Header("Spawn")]
    [SerializeField] private float spawnAhead = 12f;
    [SerializeField] private float minDelay = 1.2f;
    [SerializeField] private float maxDelay = 2.0f;

    [Header("Spawn Spacing")]
    [SerializeField] private float minSpawnDistance = 6f;
    [SerializeField] private float gapBetweenObstacles = 1.5f;

    [Header("Type Chance")]
    [Range(0f, 1f)]
    [SerializeField] private float obstacleChance = 0.25f;

    [Header("Ground")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayStartYOffset = 10f;
    [SerializeField] private float rayDistance = 200f;
    [SerializeField] private float yExtra = 0.04f;

    private float nextTime;
    private float nextSpawnX;

    [Header("Start Safety")]
    [SerializeField] private float startFreeDistance = 15f;
    private float startX;


    private bool lastWasLowBarrier;

    private bool hasNoSpawnZone;
    private float noSpawnLeftX;
    private float noSpawnRightX;


    private void Start()
    {
        if (target) startX = target.position.x;
        else startX = 0f;

        ScheduleNext();

        nextSpawnX = startX + spawnAhead + startFreeDistance;

    }

private void Update()
    {
        if (target.position.x < startX + startFreeDistance)
        {
            ScheduleNext();
            return;
        }


        if (Time.time < nextTime) return;
        if (!target) { ScheduleNext(); return; }
        if (!obstaclePrefab || !lowBarrierPrefab) { ScheduleNext(); return; }

        float xWanted = target.position.x + spawnAhead;

        if (hasNoSpawnZone && xWanted >= noSpawnLeftX && xWanted <= noSpawnRightX)
        {
            nextSpawnX = noSpawnRightX + minSpawnDistance;
            ScheduleNext();
            return;
        }

        if (hasNoSpawnZone && xWanted > noSpawnRightX)
        {
            hasNoSpawnZone = false;
        }


        if (xWanted < nextSpawnX)
        {
            ScheduleNext();
            return;
        }

        GameObject chosenPrefab;

        bool wantRoof = roofPrefab != null && Random.value < roofChance;

        if (wantRoof)
        {
            chosenPrefab = roofPrefab;
            lastWasLowBarrier = false;
        }
        else
        {
            bool wantObstacle = Random.value < obstacleChance;
            chosenPrefab = wantObstacle ? obstaclePrefab : lowBarrierPrefab;

            if (chosenPrefab == lowBarrierPrefab && lastWasLowBarrier)
                chosenPrefab = obstaclePrefab;
        }


        float x = xWanted;

        float startY = target.position.y + rayStartYOffset;
        Vector2 origin = new Vector2(x, startY);

        Debug.DrawRay(origin, Vector2.down * rayDistance, Color.yellow, 0.2f);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayDistance, groundLayer);

        if (hit.collider != null)
        {
            Debug.DrawRay(origin, Vector2.down * hit.distance, Color.green, 0.2f);

            float halfH = 0.5f;
            Collider2D prefabCol = chosenPrefab.GetComponent<Collider2D>();
            if (prefabCol != null) halfH = prefabCol.bounds.extents.y;

            Vector3 pos = new Vector3(x, hit.point.y + halfH + yExtra, 2f);
            GameObject obj = Instantiate(chosenPrefab, pos, Quaternion.identity);

            if (chosenPrefab == roofPrefab)
            {
                float widthRoof = GetPrefabWidth(roofPrefab);
                float pad = 0.5f;

                hasNoSpawnZone = true;
                noSpawnLeftX = x - widthRoof * 0.5f - pad;
                noSpawnRightX = x + widthRoof * 0.5f + pad;
            }


            AutoDestroy ad = obj.GetComponent<AutoDestroy>();
            if (ad != null) ad.SetTarget(target);

            lastWasLowBarrier = (chosenPrefab == lowBarrierPrefab);

            float width = GetPrefabWidth(chosenPrefab);
            float minStep = Mathf.Max(minSpawnDistance, width + gapBetweenObstacles);

            nextSpawnX = x + minStep;
        }
        else
        {
            Debug.LogWarning($"Raycast did not find Ground at x={x}. Check ground coverage + layer + collider.");
            nextSpawnX = xWanted + minSpawnDistance;
        }

        ScheduleNext();
    }

    private void ScheduleNext()
    {
        nextTime = Time.time + Random.Range(minDelay, maxDelay);
    }

    private float GetPrefabWidth(GameObject prefab)
    {
        if (!prefab) return 1f;

        Collider2D col = prefab.GetComponent<Collider2D>();
        if (col != null) return Mathf.Max(0.5f, col.bounds.size.x);

        SpriteRenderer sr = prefab.GetComponentInChildren<SpriteRenderer>();
        if (sr != null) return Mathf.Max(0.5f, sr.bounds.size.x);

        return 1f;
    }
}





