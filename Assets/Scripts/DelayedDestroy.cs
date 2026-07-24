using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{

    public float delay = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("DestroyGameObject", delay);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    
}
