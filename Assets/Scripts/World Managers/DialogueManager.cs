using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _dialogueBodyText;
    [SerializeField] private TextMeshProUGUI _dialogueNameText;

    private List<DialogueData> _savedDialogueDataList = new List<DialogueData>();

    private bool _dialogueRunning = false;
    private bool _dialogueProgressedThisFrame = false;
    private int _dialogueProgressionCount = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        HideDialogue();
    }

    void Update()
    {
        if(_dialogueRunning) RunDialogue();
    }

    void HideDialogue()
    {
        _dialogueBox.SetActive(false);
    }

    #region Utility
    bool ProgressDialogueButtonPressed()
    {
        return (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E));
    }

    public bool IsDialogueRunning()
    {
        return _dialogueRunning;
    }
    #endregion

    #region Dialogue Core
    void NextDialogue()
    {
        if(_dialogueProgressionCount >= _savedDialogueDataList.Count)
        {
            EndDialogue();
        }
        else
        {
            // Set the first body test
            _dialogueBodyText.text = _savedDialogueDataList[_dialogueProgressionCount].dialogueText;

            if (_savedDialogueDataList[_dialogueProgressionCount].dialogueAudio != null)
            {
                AudioManager.Instance.PlayVoiceLine(_savedDialogueDataList[_dialogueProgressionCount].dialogueAudio);
            }

            // Increment the Dialog Data List index
            _dialogueProgressionCount++;
        }
    }
    void EndDialogue()
    {
        _dialogueRunning = false;
        _dialogueBox.SetActive(false);
        EventsManager.Instance.onDialogueEnded.Invoke();
    }

    public void TriggerDialogue(string npcName, List<DialogueData> dialogDatas)
    {
        if(dialogDatas == null)
        {
            Debug.LogError("Attempted to send a null Dialog Data List to DialogManager.TriggerDialog");
        }

        // Turn the dialog box parent GameObject on
        _dialogueBox.SetActive(true);
        // Set the NPC name
        _dialogueNameText.text = npcName;
        // Restart the counter for the list infex of information to pull from
        _dialogueProgressionCount = 0;

        _savedDialogueDataList = dialogDatas;
        NextDialogue();

        // Set the dialog logic to run
        _dialogueRunning = true;
        EventsManager.Instance.onDialogueStarted.Invoke();
    }

    void RunDialogue()
    {
        if(ProgressDialogueButtonPressed() && !_dialogueProgressedThisFrame)
        {
            _dialogueProgressedThisFrame = true;
            NextDialogue();
        }
        else
        {
            _dialogueProgressedThisFrame = false;
        }
    }
    #endregion
}

[Serializable]
public class DialogueData
{
    public string dialogueText = "";
    public AudioClip dialogueAudio;
}