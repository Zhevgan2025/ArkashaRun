using UnityEngine;

public class ExplosionEvents : MonoBehaviour
{
    public void PlayExplosion()
    {
        SFXPlayer.I?.PlayExplosion();
    }
}


