using TMPro;
using UnityEngine;

public class LevelNoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelTMP;
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLevelNo(int levelNo)
    {
        levelTMP.text = "LEVEL " + levelNo;
    }
}
