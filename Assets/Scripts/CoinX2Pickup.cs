using UnityEngine;

public class CoinX2Pickup : MonoBehaviour
{
    [SerializeField] private int multiplier = 2;
    [SerializeField] private float duration = 10f;

    [SerializeField] private GameObject hudX2Icon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (FeedMultiplier.Instance != null)
            FeedMultiplier.Instance.Activate(multiplier, duration);

        if (hudX2Icon != null)
        {
            hudX2Icon.SetActive(true);
            CancelInvoke(nameof(HideIcon));
            Invoke(nameof(HideIcon), duration);
        }

        Destroy(gameObject);

        SFXPlayer.I?.PlayPickup();
        Destroy(gameObject);
    }

    private void HideIcon()
    {
        if (hudX2Icon != null)
            hudX2Icon.SetActive(false);
    }
}

