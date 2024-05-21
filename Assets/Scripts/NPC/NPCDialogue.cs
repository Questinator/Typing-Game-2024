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

    public Dialogue[] Dialogue => dialogue;
    public bool EndWithBattle => endWithBattle;
}

/// <summary>
/// The info about the dialogue.
/// </summary>
[Serializable]
public struct Dialogue
{
    public string[] text;
}
