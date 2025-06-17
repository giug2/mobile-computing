using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogues : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    public GameObject controlPanel;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    public void NextButton()
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }
    
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(Type());
        }
        else
        {
            gameObject.SetActive(false);
            controlPanel.SetActive(true);
        }
    }
}
