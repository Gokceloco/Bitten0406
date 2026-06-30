using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level : MonoBehaviour
{
    public bool isProcedural;
    public int length;

    public float levelTime;

    [SerializeField] private Tile tilePrefab;

    private Vector3 _lastTileDirection;
    private Vector3 _lastTilePosition;

    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private Enemy enemyPrefab;

    void Start()
    {
        if (isProcedural)
        {
            CreateProceduralLevel();
        }
        foreach (var e in GetComponentsInChildren<Enemy>())
        {
            e.StartEnemy();
        }
    }

    private void CreateProceduralLevel()
    {
        for (int i = 0; i < length; i++)
        {
            if (i == 0)
            {
                var newTile = Instantiate(tilePrefab, transform);
                newTile.transform.position = Vector3.zero;
                _lastTilePosition = newTile.transform.position;
            }
            if (i == 1)
            {
                var newTile = Instantiate(tilePrefab, transform);
                newTile.transform.position = new Vector3(0,0,10);
                _lastTilePosition = newTile.transform.position;
            }
            else
            {
                _lastTileDirection = ReturnRandomDirection();
                var newTile = Instantiate(tilePrefab, transform);
                newTile.transform.position = _lastTilePosition + _lastTileDirection * 10;
                _lastTilePosition = newTile.transform.position;
                if (i == length - 1)
                {
                    var newPotion = Instantiate(potionPrefab, newTile.transform);
                    newPotion.transform.localPosition = Vector3.zero;
                }
                if (Random.value < .2f)
                {
                    var newEnemy = Instantiate(enemyPrefab, newTile.transform);
                    newEnemy.transform.localPosition = Vector3.zero;
                    newEnemy.StartEnemy();
                }
            }
        }

        GetComponent<NavMeshSurface>().BuildNavMesh();

    }

    private Vector3 ReturnRandomDirection()
    {
        if (_lastTileDirection == Vector3.forward)
        {
            var randomizer = Random.Range(0,3);
            if (randomizer == 0)
            {
                return Vector3.forward;
            }
            if (randomizer == 1)
            {
                return Vector3.right;
            }
            if (randomizer == 2)
            {
                return Vector3.left;
            }
        }
        else if (_lastTileDirection == Vector3.right)
        {
            var randomizer = Random.Range(0, 2);
            if (randomizer == 0)
            {
                return Vector3.forward;
            }
            if (randomizer == 1)
            {
                return Vector3.right;
            }
        }
        else if (_lastTileDirection == Vector3.left)
        {
            var randomizer = Random.Range(0, 2);
            if (randomizer == 0)
            {
                return Vector3.forward;
            }
            if (randomizer == 1)
            {
                return Vector3.left;
            }
        }
        return Vector3.forward;
    }

    public int ReturnEnemyCount()
    {
        return GetComponentsInChildren<Enemy>().Length;
        
    }
}
