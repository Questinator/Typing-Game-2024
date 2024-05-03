using System;

public class CombatController
{
    private CombatEntity p1;
    private CombatEntity p2;

    private int turn = 0;

    public CombatController(CombatEntity p1, CombatEntity p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }
    
    /// <summary>
    /// Casts a random spell from what the attacker can cast.
    /// </summary>
    public Spell GetRandomSpell()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Casts the spell with the relevant position in the attacker's <c>spells</c> array.
    /// </summary>
    /// <param name="spellNum">The position on the attacker's <c>spells</c> array for the cast spell.</param>
    public Spell GetSpell(int spellNum)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Casts a spell.
    /// </summary>
    /// <param name="spell">Which spell to cast.</param>
    /// <returns>The Combat's Result</returns>
    public CombatResult CastSpell(Spell spell, int accuracy, int speed)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Return the result of combat from an AI.
    /// </summary>
    /// <returns></returns>
    public CombatResult doAITurn()
    { 
        throw new NotImplementedException();
    }
    
    
    public CombatResult doPlayerTurn(Spell spell, int accuracy, int speed)
    {
        throw new NotImplementedException();
    }

    public int p1Health => p1.Health;
    public int p2Health => p2.Health;

    public struct CombatResult
    {
        public Spell spell;
        public int damage;
    }
}
