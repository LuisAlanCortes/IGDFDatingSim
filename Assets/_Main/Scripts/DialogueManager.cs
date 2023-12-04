using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    // Initialization of variables.
    public CharacterAnimator anim;
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;
    static Story story;
    static Choice choiceSelected;
    TMP_Text message;
    private const string SPEAKER_TAG = "speaker";
    private const string CHAR_TAG = "chara";
    private const string SPRITE_TAG = "spr";
    private const string MC_TAG = "mc";
    private DialogueVariables dialogueVariables;
    private static DialogueManager instance;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private GameObject nameBox;
    [SerializeField] private TextAsset loadGlobalsJSON;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        if (preppedInkFile != null)
        {
            inkFile = preppedInkFile;
            preppedInkFile = null;
        }
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    static TextAsset preppedInkFile;
    public static void LoadSceneWithDialogue(TextAsset ink)
    {
        preppedInkFile = ink;
        SceneManager.LoadScene("DialogueScreen");
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        nameBox.SetActive(true);

        if (DayManager.instance)
            DayManager.instance.ConfirmValues();

        story = new Story(inkFile.text);
        dialogueVariables.StartListening(story);
        message = textBox.transform.GetChild(0).GetComponent<TMP_Text>();
        choiceSelected = null;
        displayNameText.text = "???";

    }

    private Coroutine choices;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //Is there more to the story?
            if(story.canContinue)
            {
                AdvanceDialogue();

                //Are there any choices?
                if (story.currentChoices.Count != 0)
                {
                    choices = StartCoroutine(ShowChoices());
                }
            }
            else if (choices == null)
            {
                FinishDialogue();
            }
        }
    }


    // Presents a message in the console that the INK file is expended; finishes dialogue.
    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");
        dialogueVariables.StopListening(story);

        DayManager.instance.AdvanceDay();
        SceneManager.LoadScene("Hub");
    }

    // Continues the dialogue.
    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        StopAllCoroutines();
        ParseTags(story.currentTags);
        StartCoroutine(TypeSentence(currentSentence));
    }

    // Makes the character bounce, as well as sends out dialogue letter by letter.
    IEnumerator TypeSentence(string sentence)
    {
        anim.Bounce(0.5f, 15f, 35f);
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }
    }

    // Create choices, maintain until one is selected.
    IEnumerator ShowChoices()
    {
        Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<TMP_Text>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    // Tells the story which branch to go to.
    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    // After a choice was made, turn off the panel and advance from that choice
    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choices = null;
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        AdvanceDialogue();
    }

    /*** Tag Parser ***/
    /// In Inky, you can use tags which can be used to cue stuff in a game.
    /// This is just one way of doing it. Not the only method on how to trigger events. 
    void ParseTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case CHAR_TAG:
                    anim.SetCharacter(tagValue.ToLower());
                    break;
                case SPRITE_TAG:
                    anim.ChangeSprite(tagValue.ToLower());
                    break;
                case "color":
                    SetTextColor(tagValue);
                    break;
                case MC_TAG:
                    toggleNameBox(tagValue);
                    break;
            }
        }
    }

    void toggleNameBox(string id)
    {
        switch (id)
        {
            case "true":
                nameBox.SetActive(false);
                break;
            case "false":
                nameBox.SetActive(true);
                break;
            default:
                Debug.Log("Not true or false statement.");
                break;
        }
    }
    void SetTextColor(string _color)
    {
        switch(_color)
        {
            case "red":
                message.color = Color.red;
                break;
            case "blue":
                message.color = Color.cyan;
                break;
            case "green":
                message.color = Color.green;
                break;
            case "white":
                message.color = Color.white;
                break;
            default:
                Debug.Log($"{_color} is not available as a text color");
                break;
        }
    }
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
    // this method will allow of a variable defined in globals.ink to be set using C# code
    public void SetVariableState(string variableName, Ink.Runtime.Object variableValue)
    {
        if (dialogueVariables.variables.ContainsKey(variableName))
        {
            dialogueVariables.variables.Remove(variableName);
            dialogueVariables.variables.Add(variableName, variableValue);
        }
        else
        {
            Debug.LogWarning("Tried to update variable that wasn't initialized by globals.ink: " + variableName);
        }
    }
    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit()
    {
        dialogueVariables.SaveVariables();
    }

}
