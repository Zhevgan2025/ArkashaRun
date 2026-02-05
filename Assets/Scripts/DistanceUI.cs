using TMPro;
using UnityEngine;

public class HUDTextUI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float metersFactor = 1f;

    private float startX;
    private int feedCount;


    public int SnapshotFeed { get; private set; }
    public float SnapshotMeters { get; private set; }
    public void TakeSnapshot() { SnapshotMeters = CurrentMeters; SnapshotFeed = FeedCount; }



    public static HUDTextUI Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    private void Start()
    {
        if (player) startX = player.position.x;
        UpdateText(0);
    }

    public void AddFeed(int amount = 1)
    {
        feedCount += amount;
        UpdateText(GetMeters());
    }

    private void Update()
    {
        if (!player || !text) return;
        UpdateText(GetMeters());
    }

    private float GetMeters()
    {
        float meters = (player.position.x - startX) * metersFactor;
        if (meters < 0) meters = 0;
        return meters;
    }

    private void UpdateText(float meters)
    {
        string distStr;

        if (meters < 1000f)
        {
            distStr = $"{meters:0} m";
        }
        else
        {
            float km = meters / 1000f;
            distStr = $"{km:0.0} km";
        }

        text.text = $"{distStr}\nFeed: {feedCount}";
    }
    public float CurrentMeters => GetMeters();
    public int FeedCount => feedCount;
}


