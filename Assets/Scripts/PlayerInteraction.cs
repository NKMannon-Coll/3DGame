using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] float interactionDistance = 2;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.gameObject.TryGetComponent(out Interactable interactable)) 
                {
                    Debug.Log("hit");
                }
            }
        }
    }

}
