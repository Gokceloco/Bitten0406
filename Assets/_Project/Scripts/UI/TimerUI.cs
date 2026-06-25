using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI remainingTimeTMP;
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetFillBar(float remainingTime, float startTime)
    {
        remainingTimeTMP.text = Mathf.CeilToInt(remainingTime).ToString();
        fillBar.fillAmount = remainingTime / startTime;
    }
}
