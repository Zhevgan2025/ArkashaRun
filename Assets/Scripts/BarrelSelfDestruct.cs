using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.6f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
