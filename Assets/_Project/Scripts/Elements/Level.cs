using UnityEngine;

public class Level : MonoBehaviour
{
    void Start()
    {
        foreach (var e in GetComponentsInChildren<Enemy>())
        {
            e.StartEnemy();
        }
    }
}
