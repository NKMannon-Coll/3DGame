using UnityEngine;

public class DestroyCannonball : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f);
    }
}
