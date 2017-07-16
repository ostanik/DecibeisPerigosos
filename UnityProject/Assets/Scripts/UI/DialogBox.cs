using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{

    public TextTyper dialogBox;
    public TextTyperDelegation listener;

    // Use this for initialization
    void Start () {
        
    }

    public void setupDialogBox(TextTyperDelegation _delegate)
    {
        dialogBox.delegation = _delegate;
    }

    public void clearText()
    {
        dialogBox.setText("");
    }

    public void showMessage(string msg)
    {
        dialogBox.setText(msg);
        dialogBox.StartTextAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            dialogBox.Finish();
        }
    }

}
