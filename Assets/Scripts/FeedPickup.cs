using UnityEngine;

public class FeedPickup : MonoBehaviour
{
    [SerializeField] private int amount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        int mult = 1;
        if (FeedMultiplier.Instance != null)
            mult = FeedMultiplier.Instance.CurrentMultiplier;

        if (HUDTextUI.Instance != null)
            HUDTextUI.Instance.AddFeed(amount * mult);

        Destroy(gameObject);
    }
}



