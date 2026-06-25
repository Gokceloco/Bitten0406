using UnityEngine;

public class FailUI : MonoBehaviour
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

    public void RetryButtonPressed()
    {
        gameDirector.RestartLevel();
        Hide();
    }
}
