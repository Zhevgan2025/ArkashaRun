using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private Transform target;      
    [SerializeField] private float destroyBehind = 20f; 

    private void Update()
    {
        if (!target) return;

        if (transform.position.x < target.position.x - destroyBehind)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform t) => target = t;
}

