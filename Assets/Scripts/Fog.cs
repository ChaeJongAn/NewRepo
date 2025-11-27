using UnityEngine;

public class Fog : MonoBehaviour
{
    public float lifetime = 15f;

    protected virtual void OnEnable()
    {
        Invoke("Disable", lifetime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        // 자식에서 구현
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}