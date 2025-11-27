// WaterTile.cs - 물 타일에 붙이기
using UnityEngine;

public class WaterTile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StatusEffect status = other.GetComponent<StatusEffect>();
            if (status != null)
            {
                status.ApplyWet();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StatusEffect status = other.GetComponent<StatusEffect>();
            if (status != null)
            {
                status.RemoveWet();
            }
        }
    }
}


