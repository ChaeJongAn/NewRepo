// WaterTileDetector.cs - 플레이어에 붙이기
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterTileDetector : MonoBehaviour
{
    public Tilemap waterTilemap;
    StatusEffect _status;

    void Start()
    {
        _status = GetComponent<StatusEffect>();
    }

    void Update()
    {
        if (waterTilemap == null || _status == null) return;

        // 플레이어 위치를 타일 좌표로 변환
        Vector3Int cellPos = waterTilemap.WorldToCell(transform.position);

        // 해당 위치에 타일이 있는지 확인
        TileBase tile = waterTilemap.GetTile(cellPos);

        if (tile != null)
        {
            _status.ApplyWet();
        }
        else
        {
            _status.RemoveWet();
        }
    }
}