using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField] bool uiOn = false;
    [SerializeField] bool cursorOff = false;
    [SerializeField] bool startingDialogueNeeded = false;
    [SerializeField] Dialogue startingDialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.setUI(uiOn);
        if (cursorOff)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (startingDialogueNeeded) 
        {
            GameManager.Instance.StartDialogue(startingDialogue.dialogue, 0, "Random Voice.....");
        }
        
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void startGame() 
    {
        GameManager.Instance.startGame();
    }

    public void goToCredits() 
    {
        GameManager.Instance.credits();
    }

    public void goToControls() 
    {
        GameManager.Instance.controls();
    }

    public void goToMainMenu() 
    {
        GameManager.Instance.mainMenu();
    }


}
