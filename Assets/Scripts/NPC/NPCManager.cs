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
        public string characterTitle;
        public GameObject textBox;
        public TextMeshProUGUI dialogueText;
        public TextMeshProUGUI characterNameText;
        public TextMeshProUGUI characterTitleText;
        NPCAnimatorManager npcAnimatorManager;
        PlayerAnimatorManager playerAnimatorManager;
        UIManager uiManager;
        public Rigidbody npcRigidbody;
        public int sentenceCounter = 0;

        private void Awake()
        {
            npcAnimatorManager = GetComponentInChildren<NPCAnimatorManager>();
            npcRigidbody = GetComponent<Rigidbody>();
            uiManager = FindObjectOfType<UIManager>();
            playerAnimatorManager = FindObjectOfType<PlayerAnimatorManager>();
            characterNameText.text = characterName;
            characterTitleText.text = characterTitle;
        }

        public void StartDialogue()
        {
            uiManager.interactAlertPopUpOnTrigger.SetActive(false);
            textBox.SetActive(true);
            sentenceCounter = 0;
            dialogueText.text = sentences[sentenceCounter];
            playerAnimatorManager.anim.SetBool("isInteracting", true);
            playerAnimatorManager.anim.SetBool("canRotate", false);
            PlayTalkingAnimation();
        }

        public void ContinueDialogue()
        {
            sentenceCounter++;
            
            if (sentenceCounter >= sentences.Count)
            {
                uiManager.interactAlertPopUpOnTrigger.SetActive(true);
                textBox.SetActive(false);
                playerAnimatorManager.anim.SetBool("isInteracting", false);
                playerAnimatorManager.anim.SetBool("canRotate", true);
            }
            else
            {
                dialogueText.text = sentences[sentenceCounter];
                PlayTalkingAnimation();
            }
        }

        private void PlayTalkingAnimation()
        {
            if (npcAnimatorManager.randomizeAnimation)
            {
                string talkAnimation = npcAnimatorManager.talkAnimations[Random.Range(0, npcAnimatorManager.talkAnimations.Count)];
                npcAnimatorManager.anim.Play(talkAnimation);
            }
            else
            {
                npcAnimatorManager.anim.Play(npcAnimatorManager.talkAnimations[sentenceCounter]);
            }
        }

    }
}
