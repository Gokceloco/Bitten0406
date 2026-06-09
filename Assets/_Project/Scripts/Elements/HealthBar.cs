using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform fillBarPivot;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetFillBar(int startHealth, int currentHealth)
    {
        var ratio = currentHealth / (float)startHealth;
        fillBarPivot.localScale = new Vector3(ratio, 1, 1);
        if (ratio >= 1 || ratio <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(45,0,0);
    }
}
