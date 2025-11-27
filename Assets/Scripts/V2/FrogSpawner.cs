using UnityEngine;
using UnityEngine.Tilemaps;

public class FrogSpawner : MonoBehaviour
{
    public Tilemap waterTilemap;
    public GameObject frogPrefab;
    public float spawnInterval = 10f;
    public int maxFrogs = 3;

    float _timer;
    int _currentFrogs = 0;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval && _currentFrogs < maxFrogs)
        {
            SpawnFrog();
            _timer = 0f;
        }
    }

    void SpawnFrog()
    {
        // 물 타일 랜덤 위치 찾기
        BoundsInt bounds = waterTilemap.cellBounds;

        for (int i = 0; i < 100; i++)  // 100번 시도
        {
            int x = Random.Range(bounds.xMin, bounds.xMax);
            int y = Random.Range(bounds.yMin, bounds.yMax);
            Vector3Int cellPos = new Vector3Int(x, y, 0);

            if (waterTilemap.HasTile(cellPos))
            {
                Vector3 worldPos = waterTilemap.CellToWorld(cellPos) + new Vector3(0.5f, 0.5f, 0);
                GameObject frog = Instantiate(frogPrefab, worldPos, Quaternion.identity);
                frog.GetComponent<FrogNPC>().isWild = true;
                _currentFrogs++;
                return;
            }
        }
    }

    // 개구리 죽으면 카운트 감소
    public void OnFrogDied()
    {
        _currentFrogs--;
    }
}