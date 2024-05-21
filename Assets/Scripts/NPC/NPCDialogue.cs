using System;
using UnityEngine;

/// <summary>
/// The dialogue to be used on an NPC, and some extra info.
/// </summary>
[CreateAssetMenu]
public class NPCDialogue : ScriptableObject
{
    [SerializeField] private Dialogue[] dialogue;
    [SerializeField] private bool endWithBattle;
    [SerializeField] private NPCDialogue victoryDialogue;
    [SerializeField] private string loseScene;
    [SerializeField] private CombatEntity player;
    [SerializeField] private CombatEntity enemy;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject enemyModel;


    public GameObject PlayerModel => playerModel;

    public GameObject EnemyModel => enemyModel;

    public Dialogue[] Dialogue => dialogue;
    public bool EndWithBattle => endWithBattle;

    public NPCDialogue VictoryDialogue => victoryDialogue;

    public string LoseScene => LoseScene;

    public CombatEntity Player => player;

    public CombatEntity Enemy => enemy;
}

/// <summary>
/// The info about the dialogue.
/// </summary>
[Serializable]
public struct Dialogue
{
    public string[] text;
}
