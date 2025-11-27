using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack2 : MonoBehaviour
{
    public GameObject knifePrefab;
    public float knifeSpeed = 8f;

    Inventory _inventory;

    void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ShootKnife();
        }
    }

    void ShootKnife()
    {
        // 단검 있는지 확인
        if (_inventory != null && !_inventory.UseKnife())
        {
            Debug.Log("단검 없음!");
            return;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;

        GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
        knife.GetComponent<Knife>().direction = dir;
    }
}