using UnityEngine;

public class RoofStartOnTouch : MonoBehaviour
{
    [SerializeField] private RoofLooper roofLooper;
    [SerializeField] private string playerTag = "Player";

    private int contacts;

    private void Awake()
    {
        if (!roofLooper) roofLooper = GetComponentInParent<RoofLooper>();
        if (roofLooper) roofLooper.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        contacts++;
        if (roofLooper) roofLooper.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        contacts = Mathf.Max(0, contacts - 1);
        if (contacts == 0 && roofLooper) roofLooper.enabled = false;
    }
}
