using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField] bool uiOn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.setUI(uiOn);
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
