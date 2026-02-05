using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text statsText;
    [SerializeField] private string gameSceneName = "SampleScene";

    private void Start()
    {
        statsText.text =
            $"Best Distance: {Stats.BestDistance:0}\n" +
            $"Total Feed: {Stats.TotalFeed}";
    }

    public void Play() => SceneManager.LoadScene(gameSceneName);
    public void Exit() { Application.Quit(); Debug.Log("Exit"); }
}
