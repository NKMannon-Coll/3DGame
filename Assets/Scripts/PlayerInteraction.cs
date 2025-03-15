using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] float interactionDistance = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void FixedUpdate()
    {
       
    }

    [SerializeField] float interactDistance = 2;
    bool inConversation = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("yum");
            Interact();
        }
    }

    void Interact()
    {
        if (inConversation)
        {
            Debug.Log("Skipping Line");
            GameManager.Instance.SkipLine();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.transform.gameObject.TryGetComponent(out Interactable interactable))
                    {
                        GameManager.Instance.StartDialogue(interactable.dialogue.dialogue, interactable.StartPosition, interactable.npcName);
                    }
                }
            }
        }
    }

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
    }

    private void OnEnable()
    {
        GameManager.OnDialogueStarted += JoinConversation;
        GameManager.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        GameManager.OnDialogueStarted -= JoinConversation;
        GameManager.OnDialogueEnded -= LeaveConversation;
    }

}
