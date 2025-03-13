using UnityEngine;

public class EnemyCannonball : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out PlayerBodyStayPut pm)) 
        {
            GameManager.Instance.changeHealth(-20);
        }
    }
}
