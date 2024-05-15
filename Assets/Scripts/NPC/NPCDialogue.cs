using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : ScriptableObject
{
    public Dialogue[] dialogue;
}

public class Dialogue
{
    public string[] text;
}
