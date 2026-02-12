using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer I;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pickupClip;
    [Range(0f, 1f)][SerializeField] private float volume = 1f;

    [SerializeField] private AudioClip explosionClip;

    public void PlayExplosion()
    {
        if (!source || !explosionClip) return;
        source.PlayOneShot(explosionClip, volume);
    }


    private void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;

        if (!source) source = GetComponent<AudioSource>();
    }

    public void PlayPickup()
    {
        if (!source || !pickupClip) return;
        source.PlayOneShot(pickupClip, volume);
    }
}

