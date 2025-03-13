using UnityEngine;

public class PlayerBodyStayPut : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position - new Vector3(0,0.25f,0);
    }
}
