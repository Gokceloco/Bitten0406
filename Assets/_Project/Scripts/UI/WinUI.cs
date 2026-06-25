using UnityEngine;

public class WinUI : MonoBehaviour
{
    public GameDirector gameDirector;
    public void Show(float delay)
    {
        Invoke(nameof(SetActiveTrue), delay);
    }
    void SetActiveTrue()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void LoadNextLevelButtonPressed()
    {
        gameDirector.LoadNextLevel();
        Hide();
    }
}
