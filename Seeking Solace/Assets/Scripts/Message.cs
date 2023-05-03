using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public GameObject panel;
    public Text text;

    private void Start()
    {
        // Disable the panel and text object on start
        panel.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        // Set the text of the text object to the given message
        text.text = message;

        // Enable the panel and text object
        panel.SetActive(true);
        text.gameObject.SetActive(true);

        //wait();
    }

    public void HideMessage()
    {
        // Disable the panel and text object
        panel.SetActive(false);
        text.gameObject.SetActive(false);
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
    }
}