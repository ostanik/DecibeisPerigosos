using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTyper : MonoBehaviour {

    public float letterPause = 0.2f;
    public AudioClip typeSound1;
    public AudioClip typeSound2;
    public bool isFinished = false;

    string message;
    Text textComp;

    // Use this for initialization
    void Start()
    {
        
    }

    public void StartTextAnimation()
    {
        textComp = GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        StartCoroutine(TypeText());
    }

    public void Finish()
    {
        textComp.text = message;
        isFinished = true;
        StopAllCoroutines();
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            if (textComp.text.Length == message.Length)
            {
                isFinished = true;
            }
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }
}
