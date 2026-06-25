using UnityEngine;
using UnityEngine.InputSystem;

public class Level : MonoBehaviour
{
    public float levelTime;
    void Start()
    {
        foreach (var e in GetComponentsInChildren<Enemy>())
        {
            e.StartEnemy();
        }
    }    

    public int ReturnEnemyCount()
    {
        return GetComponentsInChildren<Enemy>().Length;
        
    }
}
