using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField] bool uiOn = false;
    [SerializeField] bool cursorOff = false;

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
