using UnityEngine;
using UnityEngine.InputSystem;

public class Level : MonoBehaviour
{
    void Start()
    {
        foreach (var e in GetComponentsInChildren<Enemy>())
        {
            e.StartEnemy();
        }
    }

    private void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            print(ReturnEnemyCount());
        }
    }

    public int ReturnEnemyCount()
    {
        return GetComponentsInChildren<Enemy>().Length;
        
    }
}
