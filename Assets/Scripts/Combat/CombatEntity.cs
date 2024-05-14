using UnityEngine;

[CreateAssetMenu]
public class CombatEntity : ScriptableObject
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int level;
    [SerializeField]
    private Spell[] spells;
    
    /// <summary>
    /// The amount of HP this character has left
    /// </summary>
    public int Health => health;
    
    /// <summary>
    /// The max health for this charachter
    /// </summary>
    public int MaxHealth => maxHealth;
    
    /// <summary>
    /// The level of this entity.
    /// </summary>
    public int Level => level;
    
    /// <summary>
    /// All spells this entity can use in combat.
    /// </summary>
    public Spell[] Spells => spells;
    
    /// <summary>
    /// Damages this entity for the given damage.
    /// </summary>
    /// <param name="damage">How much to damage this entity.</param>
    public void Damage(int damage)
    {
        health -= damage;
    }
    /// <summary>
    /// Creates a copy of this scriptable object, if you have an asset
    /// </summary>
    /// <returns></returns>
    public CombatEntity Clone()
    {
        return Instantiate(this);
    }
}
