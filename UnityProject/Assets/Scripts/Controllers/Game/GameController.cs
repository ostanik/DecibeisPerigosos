using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public string[] dialogs;

    public bool isSafeSound;
    public int currentScreen = 0;
    public int currentDialog = 0;
    public bool canShowNextScreen = false;

    public AudioClip sceneAudio;
    public AudioClip backgroundAudio;
    public DialogBox dialogBox;
    public QuestionBox questionBox;
    public GameScreenController gameController;

    virtual public void Setup()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        questionBox = FindObjectOfType<QuestionBox>();

        if (questionBox != null) {
            questionBox.currentGame++;
        }
 
        gameController = FindObjectOfType<GameScreenController>();
    }

}