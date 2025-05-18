using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int health = 100;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    private AudioSource audioSource;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip fallen;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void changeHealth(int deltaHealth)
    {
        health += deltaHealth;
        healthText.text = "Health: " + health;
        if (health <= 0)
        {
            mainMenu();
            audioSource.PlayOneShot(fallen, 0.3f);
        }
        else
        {
            audioSource.PlayOneShot(hit, 0.3f);
        }
    }

    public int enemyCount;

    public void changeScore(int deltaScore)
    {
        score += deltaScore;
        scoreText.text = "Score: " + score;
    }
    public int score;

    [SerializeField] GameObject UI;

    public void setUI(bool isOn)
    {
        UI.SetActive(isOn);
    }
    public void mainMenu()
    {
        Initiate.Fade("MainMenu", Color.black, 1);
    }

    public void credits()
    {
        Initiate.Fade("Credits", Color.black, 1);
    }

    public void controls()
    {
        Initiate.Fade("Controls", Color.black, 1);
    }

    public void startGame()
    {
        Initiate.Fade("IanScene", Color.black, 1);
    }
    //add back in when have UI

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;


    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;
    public void StartDialogue(string[] dialogue, int startPosition, string name)
    {
        nameText.text = name + "...";
        dialoguePanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue, startPosition));
    }

    IEnumerator RunDialogue(string[] dialogue, int startPosition)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();

        for (int i = startPosition; i < dialogue.Length; i++)
        {
            //dialogueText.text = dialogue[i];
            dialogueText.text = null;
            StartCoroutine(TypeTextUncapped(dialogue[i]));

            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialoguePanel.SetActive(false);
        dialogueText.text = null;
        nameText.text = null;
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        StartCoroutine(TypeTextUncapped(dialogue));
        dialoguePanel.SetActive(true);
    }

    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        dialoguePanel.SetActive(false);
    }

    float charactersPerSecond = 90;

    IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;

        while (i < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
