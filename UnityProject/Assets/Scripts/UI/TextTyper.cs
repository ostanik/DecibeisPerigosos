using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface TextTyperDelegation {
    void didFinishAnimation();
}

public class TextTyper : MonoBehaviour {

    public float letterPause = 0.2f;
    public bool isFinished = false;
    public TextTyperDelegation delegation;

    string message;
    Text textComp;

    // Use this for initialization
    void Start()
    {
        
    }

    public void setText(string text)
    {
        GetComponent<Text>().text = text.Replace("\\n", "\n");
        isFinished = false;
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
        if (message != null) {
            textComp.text = message;
            isFinished = true;
            delegation.didFinishAnimation();
        }
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
                if (delegation != null)
                {
                    delegation.didFinishAnimation();
                }
            }
            yield return new WaitForSeconds(letterPause);
        }
    }
}
