using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Animator animator;
    public Image imageHolder;
    public AudioClip sound;

    private Queue<Sentence> sentences;
    
    void Start()
    {
        sentences = new Queue<Sentence>();
    }

    //This functions is called by dialogue triggers and it start a dialogue
    public void StartDialogue(Dialogue d)
    {
        animator.SetBool("isOpen", true);
        imageHolder = d.imageHolder;
        imageHolder.preserveAspect = true;
        sound = d.sound;

        sentences.Clear();

        foreach(Sentence sentence in d.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        StopAllCoroutines();
        imageHolder.sprite = sentence.characterSprite;
        nameText.text = sentence.name;
        StartCoroutine(TypeSentence(sentence.sentenceText));
    }

    //The typing coroutine of the function types sentences making chosen sound for each character
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            SoundManager.instance.PlaySound(sound);
            yield return new WaitForSeconds(0.1f); 
        }

        yield return new WaitForSeconds(0.7f);
        DisplayNextSentence();
    }

    //This function animates the closing of the dialogue box
    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
