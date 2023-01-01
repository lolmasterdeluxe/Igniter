using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace IG
{
    public class NPCManager : CharacterManager
    {
        public List<string> sentences;
        public string characterName;
        public GameObject textBox;
        public TextMeshProUGUI dialogueText;
        public TextMeshProUGUI characterNameText;
        NPCAnimatorManager animatorManager;
        public Rigidbody npcRigidbody;
        public int sentenceCounter = 0;

        private void Awake()
        {
            animatorManager = GetComponentInChildren<NPCAnimatorManager>();
            npcRigidbody = GetComponent<Rigidbody>();
            characterNameText.text = characterName;
        }

        public void StartDialogue()
        {
            textBox.SetActive(true);
            sentenceCounter = 0;
            dialogueText.text = sentences[sentenceCounter];
            PlayTalkingAnimation();
        }

        public void ContinueDialogue()
        {
            sentenceCounter++;
            
            if (sentenceCounter >= sentences.Count)
            {
                textBox.SetActive(false);
            }
            else
            {
                dialogueText.text = sentences[sentenceCounter];
                PlayTalkingAnimation();
            }
        }

        private void PlayTalkingAnimation()
        {
            if (animatorManager.randomizeAnimation)
            {
                string talkAnimation = animatorManager.talkAnimations[Random.Range(0, animatorManager.talkAnimations.Count)];
                animatorManager.anim.Play(talkAnimation);
            }
            else
            {
                animatorManager.anim.Play(animatorManager.talkAnimations[sentenceCounter]);
            }
        }

    }
}
