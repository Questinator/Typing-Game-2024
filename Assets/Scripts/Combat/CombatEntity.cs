using UnityEngine;

[CreateAssetMenu]
public class CombatEntity : ScriptableObject
{
    private int health;

    private Spell[] spells;
    
    /// <summary>
    /// The amount of HP this character has left
    /// </summary>
    public int Health => health;
    
    /// <summary>
    /// All spells this entity can use in combat.
    /// </summary>
    public Spell[] Spells => spells;
}
