using System.Collections;
using UnityEngine;

public class FeedMultiplier : MonoBehaviour
{
    public static FeedMultiplier Instance { get; private set; }

    [Header("Base")]
    [SerializeField] private int baseMultiplier = 1;

    [Header("UI")]
    [SerializeField] private GameObject x2Icon;

    private int currentMultiplier;
    private Coroutine routine;

    public int CurrentMultiplier => currentMultiplier;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        currentMultiplier = baseMultiplier;

        if (x2Icon != null) x2Icon.SetActive(false);
    }

    public void Activate(int multiplier, float duration)
    {
        currentMultiplier = Mathf.Max(baseMultiplier, multiplier);

        if (x2Icon != null) x2Icon.SetActive(true);

        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(Timer(duration));
    }

    private IEnumerator Timer(float duration)
    {
        yield return new WaitForSeconds(duration);

        currentMultiplier = baseMultiplier;

        if (x2Icon != null) x2Icon.SetActive(false);

        routine = null;
    }
}

