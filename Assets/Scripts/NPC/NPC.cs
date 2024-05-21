using System;
using System.Collections;
using System.Collections.Generic;
using Combat.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCDialogue npcDialogue;
    [SerializeField] private Collider interactCollider;

    private GameController gameController;
    private UIController uiController;

    // The current dialogue that we are on
    private int currentDialogueSet;
    private int currentDialogueText;

    private bool playerInsideCollider;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uiController = gameController.UIController;
    }

    private void Update()
    {
        if (!playerInsideCollider) return;
        
        // When the player interacts, advance the conversation
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameController.Player.CutsceneState = true;
            AdvanceConversation();
        }
    }

    /// <summary>
    /// Essentially just code from last year slightly modified to continue a conversation
    /// </summary>
    private void AdvanceConversation()
    {
        string[] textArray = npcDialogue.Dialogue[currentDialogueSet].text;

        if (textArray == null)
        {
            throw new NullReferenceException("There should be text inside of the dialogue!");
        }

        if (currentDialogueText >= textArray.Length)
        {
            // Go back to 0
            currentDialogueText = 0;

            // If we're not past the last piece of dialogue in the set go to the next one
            if (currentDialogueSet < npcDialogue.Dialogue.Length - 1)
            {
                currentDialogueSet++;
            }

            gameController.Player.CutsceneState = false;

            // Stop the loop
            uiController.CloseDialogueBox();

            if (npcDialogue.EndWithBattle)
            {
                CombatLoader.Instance.Load(new CombatInfo(npcDialogue.PlayerModel,npcDialogue.EnemyModel,npcDialogue.Player,npcDialogue.Enemy),
                    result =>
                    {
                        if (result.PlayerWon)
                        {
                            npcDialogue = npcDialogue.VictoryDialogue;
                        } 
                        else if (result.PlayerRan)
                        {
                            
                        }
                        else
                        {
                            SceneManager.LoadScene(npcDialogue.LoseScene);
                        }
                        
                    });
            }

            return;
        }

        uiController.WriteDialogueBoxText(textArray[currentDialogueText]);
        Debug.Log("Current NPC Text: " + textArray[currentDialogueText]);

        currentDialogueText++;
    }

    // This trigger stuff is kinda dumb but it works :)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = false;
        }
    }
}
