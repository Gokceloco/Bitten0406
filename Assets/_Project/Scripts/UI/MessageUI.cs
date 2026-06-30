using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI msgTMP;
    public void Show(string msg, float duration)
    {
        gameObject.SetActive(true);
        msgTMP.text = msg;
        Invoke(nameof(Hide), duration);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
