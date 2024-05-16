using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    // Ex. myLogger.Log(kTag, "Log String");
    private Logger myLogger;
    private static string kTag = "myLogger";

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private bool isDisplayingChoices = false;
    private bool isExiting = false;

    private static DialogueManager instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        //Initialize logger
        myLogger = new Logger(Debug.unityLogger.logHandler);
        myLogger.Log(kTag, "Starting Dialogue Manager...");

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //Initialize choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (Physics.Raycast(raycast, out hit) && touch.phase == TouchPhase.Began)
            {
                if (hit.collider.tag == "ExitButton")
                {
                    isExiting = true;
                    myLogger.Log(kTag, "Touched Exit Button");
                }
            }
        }

        if (!dialogueIsPlaying)
        {
            return;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isDisplayingChoices && !isExiting)
        {
            myLogger.Log(kTag, "Continuing from UPDATE...");
            ContinueStory();
        }
    }



    public void EnterDialogueMode(TextAsset inkJSON)
    {
        isExiting = false;
        myLogger.Log(kTag, "Inside EnterDialogueMode...");
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        StartCoroutine(WaitToContinue());
    }

    private IEnumerator WaitToContinue()
    {
        yield return new WaitForSeconds(0.2f);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            myLogger.Log(kTag, "Continuing from ENTER...");
            ContinueStory();
        }
    }

    private void ContinueStory()
    {
        myLogger.Log(kTag, "Inside ContinueStory...");
        if (currentStory.canContinue)
        {
            myLogger.Log(kTag, "ContinueStory... - CONTINUING");
            dialogueText.text = currentStory.Continue();
            if (dialogueText.text == "")
            {
                myLogger.Log(kTag, "Continue Story... - IF EXIT");
                ExitDialogueMode();
            }
            DisplayChoices();
        }
        else
        {
            myLogger.Log(kTag, "ContinueStory... - EXITING");
            ExitDialogueMode();
        }
    }

    public void ExitDialogueMode()
    {
        isExiting = true;
        myLogger.Log(kTag, "Inside ExitDialogueMode...");
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        currentStory = null;
        isDisplayingChoices = false;
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        myLogger.Log(kTag, "Displaying Choice..." + currentChoices.Count);

        isDisplayingChoices = currentChoices.Count > 0 ? true : false;

        if (currentChoices.Count > choices.Length)
        {
            myLogger.Log(kTag, "More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        myLogger.Log(kTag, "Making Choice..." + choiceIndex);
        isDisplayingChoices = false;
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }


}
