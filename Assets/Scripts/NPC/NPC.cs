using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCDialogue npcDialogue;
    [SerializeField] private Collider interactCollider;

    private Player player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Player;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            player.CutsceneState = true;
        }
    }
}
