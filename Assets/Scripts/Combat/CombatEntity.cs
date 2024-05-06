using UnityEngine;

[CreateAssetMenu]
public class CombatEntity : ScriptableObject
{
    private int health;
    private int level;
    private Spell[] spells;
    
    /// <summary>
    /// The amount of HP this character has left
    /// </summary>
    public int Health => health;
    
    /// <summary>
    /// The level of this entity.
    /// </summary>
    public int Level => level;
    
    /// <summary>
    /// All spells this entity can use in combat.
    /// </summary>
    public Spell[] Spells => spells;
}
