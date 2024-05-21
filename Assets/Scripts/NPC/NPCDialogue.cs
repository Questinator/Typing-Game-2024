using System;
using UnityEngine;

/// <summary>
/// The dialogue to be used on an NPC, and some extra info.
/// </summary>
[CreateAssetMenu]
public class NPCDialogue : ScriptableObject
{
    [SerializeField] private Dialogue[] dialogue;

    public Dialogue[] Dialogue => dialogue;
}

/// <summary>
/// The info about the dialogue.
/// </summary>
[Serializable]
public struct Dialogue
{
    public string[] text;
}
