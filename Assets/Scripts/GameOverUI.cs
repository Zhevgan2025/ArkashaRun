using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text runStatsText;
    [SerializeField] private HUDTextUI hud;
    private bool saved;

    private void Awake()
    {
        if (!panel) panel = gameObject;
        panel.SetActive(false);
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }


    public void Show()
    {


        hud.TakeSnapshot();
        runStatsText.text = $"Distance: {hud.SnapshotMeters:0} m\nFeed: {hud.SnapshotFeed}";



        if (hud != null)
        {
            runStatsText.text =
                $"Distance: {hud.CurrentMeters:0} m\n" +
                $"Feed: {hud.FeedCount}";

            if (!saved)
            {
                Stats.SaveRun(hud.CurrentMeters, hud.FeedCount);
                saved = true;
            }
        }

        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit натиснуто (в Unity Editor гра не закриЇтьс€ Ч це ок)");
    }
}
