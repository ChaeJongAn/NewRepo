using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public int knifeCount = 5;
    public int frogCount = 0;
    public bool hasLantern = false;
    public bool hasGhostSword = false;

    public GameObject frogPrefab;

    void Update()
    {
        // Q虐肺 俺备府 家券 (New Input System)
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            UseFrog();
        }
    }

    public bool UseKnife()
    {
        if (knifeCount > 0)
        {
            knifeCount--;
            return true;
        }
        return false;
    }

    public void UseFrog()
    {
        if (frogCount > 0 && frogPrefab != null)
        {
            frogCount--;
            Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * 1f;
            Instantiate(frogPrefab, spawnPos, Quaternion.identity);
            Debug.Log("俺备府 家券!");
        }
    }

    public void AddKnife(int amount)
    {
        knifeCount += amount;
    }

    public void AddFrog()
    {
        frogCount++;
    }

    public void GetLantern()
    {
        hasLantern = true;
    }

    public void GetGhostSword()
    {
        hasGhostSword = true;
    }
}