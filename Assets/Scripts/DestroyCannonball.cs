using UnityEngine;

public class DestroyCannonball : MonoBehaviour
{
    void Start()
    {
        GameObject wall = GameObject.FindGameObjectWithTag("Wall");     
        Physics.IgnoreCollision(wall.GetComponent<Collider>(), GetComponent<Collider>());
        Destroy(gameObject, 2f);
    }
}
