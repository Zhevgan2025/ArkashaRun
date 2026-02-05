using UnityEngine;

public static class Stats
{
    const string BEST_DISTANCE = "best_distance";
    const string TOTAL_FEED = "total_feed";

    public static float BestDistance => PlayerPrefs.GetFloat(BEST_DISTANCE, 0f);
    public static int TotalFeed => PlayerPrefs.GetInt(TOTAL_FEED, 0);

    public static void SaveRun(float distance, int feedThisRun)
    {
        if (distance > BestDistance) PlayerPrefs.SetFloat(BEST_DISTANCE, distance);
        PlayerPrefs.SetInt(TOTAL_FEED, TotalFeed + feedThisRun);
        PlayerPrefs.Save();
    }
}

