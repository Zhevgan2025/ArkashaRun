using UnityEngine;

public class ExplodeOnHit : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    public void Explode()
    {
        if (!explosionPrefab) return;

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}

