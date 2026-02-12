using UnityEngine;

public class CoinSpeedPickup : MonoBehaviour
{
    [SerializeField] private float speedAdd = 2f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovement pm = other.GetComponentInParent<PlayerMovement>();
        if (pm != null)
        {
            pm.AddSpeed(speedAdd, duration);
        }

        Destroy(gameObject);

        SFXPlayer.I?.PlayPickup();
        Destroy(gameObject);
    }
}

