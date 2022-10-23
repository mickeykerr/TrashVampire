using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnerTile : MonoBehaviour
{
    public BossController summoner; // haha league of legends.
    public GameObject SpawnItem;
    public int EnemiesPerSpawn = 1;
    public int MaxEnemies = 5;

    private List<GameObject> _activeEnemies = new List<GameObject>();
    private Tilemap _tileMap;
    private List<Vector3> _availablePlaces = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        _tileMap = GetComponent<Tilemap>();
        for (int n = _tileMap.cellBounds.xMin; n < _tileMap.cellBounds.xMax; n++)
        {
            for (int p = _tileMap.cellBounds.yMin; p < _tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)_tileMap.transform.position.y));
                Vector3 place = _tileMap.CellToWorld(localPlace);
                if (_tileMap.HasTile(localPlace))
                {
                    _availablePlaces.Add(place + new Vector3(0.5f, 0.5f));
                    Debug.Log(place);
                }
            }
        }

        summoner.OnBossSummonAbility += SpawnEntity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEntity()
    {
        _activeEnemies.RemoveAll(enemy => enemy == null);

        for (int i = 0; i < _availablePlaces.Count && _activeEnemies.Count < MaxEnemies; i++)
        {
            _activeEnemies.Add(Instantiate(SpawnItem, _availablePlaces[i], Quaternion.identity));
        }
    }
}
