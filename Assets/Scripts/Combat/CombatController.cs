using System;

public class CombatController
{
    private Random rand;
    
    private CombatEntity p1;
    private CombatEntity p2;

    private int turn;

    public CombatController(CombatEntity p1, CombatEntity p2)
    {
        this.p1 = p1;
        this.p2 = p2;

        turn = 0;
        rand = new Random();
    }
    
    /// <summary>
    /// Casts a random spell from what the attacker can cast.
    /// </summary>
    public Spell GetRandomSpell()
    {
        CombatEntity entity = getCurrentEntity();
        return entity.Spells[rand.Next(0, entity.Spells.Length)];
    }

    /// <summary>
    /// Gets the spell with the relevant position in the attacker's <c>spells</c> array.
    /// </summary>
    /// <param name="spellNum">The position on the attacker's <c>spells</c> array for the spell.</param>
    public Spell GetSpell(int spellNum)
    {
        CombatEntity entity = getCurrentEntity();
        return entity.Spells[spellNum];
    }

    /// <summary>
    /// Gets the spell with the relevant position in <c>entity</c>'s <c>spells</c> array.
    /// </summary>
    /// <param name="spellNum">The position in <c>entity</c>'s <c>spells</c> array for the spell.</param>
    /// <param name="entity">The entity to get the spell from.</param>
    /// <returns></returns>
    public Spell GetSpell(int spellNum, CombatEntity entity)
    {
        return entity.Spells[spellNum];
    }
    
    /// <summary>
    /// Casts a spell.
    /// </summary>
    /// <param name="spell">Which spell to cast.</param>
    /// <param name="accuracy">The accuracy of the typing.</param>
    /// <param name="speed">The speed of the typing.</param>
    /// <returns>The Combat's Result.</returns>
    public CombatResult CastSpell(Spell spell, int accuracy, int speed)
    {
        CombatEntity entity = getCurrentEntity();
        switchTurn();
        int damage = spell.Damage;
        if (accuracy >= spell.MinAccuracy && speed >= spell.MinSpeed)
        {
            if (accuracy >= spell.CritAccuracyThreshold && speed >= spell.CritSpeedThreshhold)
            {
                damage = (int)(damage * spell.CritMultiplier);
            }
        }
        else
        {
            damage = 0;
        }
        entity.Damage(damage);
        
        return new CombatResult(spell, damage);
    }
    
    /// <summary>
    /// Return the result of combat from an AI.
    /// </summary>
    /// <returns>The result of combat from the AI.</returns>
    public CombatResult doAITurn()
    {
        CombatEntity entity = getCurrentEntity();
        Spell spell = entity.Spells[rand.Next(0, entity.Spells.Length)];
        if (entity.Level <= 3)
        {
            return CastSpell(spell, rand.Next(75, 85), rand.Next(10, 35));
        } else if (entity.Level <= 6)
        {
            return CastSpell(spell, rand.Next(80, 90), rand.Next(20, 40));
        } else if (entity.Level <= 9)
        {
            return CastSpell(spell, rand.Next(85, 90), rand.Next(30, 50));
        } else if (entity.Level > 9)
        {
            return CastSpell(spell, rand.Next(95, 100), rand.Next(40, 50));
        }
        // It should never get here because the ifs encompass all values.
        throw new ArgumentException("something isn't working right....");
    }
    
    
    public CombatResult doPlayerTurn(Spell spell, int accuracy, int speed)
    {
        return CastSpell(spell, accuracy, speed);
    }
    
    /// <summary>
    /// Gets the CombatEntity whose turn it is.
    /// </summary>
    /// <returns>The CombatEntity whose turn it is.</returns>
    private CombatEntity getCurrentEntity()
    {
        if (turn == 0)
        {
            return p1;
        }
        else
        {
            return p2;
        }
    }
    
    /// <summary>
    /// Switches the current turn.
    /// </summary>
    private void switchTurn()
    {
        if (turn == 0)
        {
            turn = 1;
        }
        else
        {
            turn = 0;
        }
    }
    public int p1Health => p1.Health;
    public int p2Health => p2.Health;
    
    /// <summary>
    /// The result of combat.
    /// </summary>
    public struct CombatResult
    {
        public Spell spell;
        public int damage;

        public CombatResult(Spell spell, int damage)
        {
            this.spell = spell;
            this.damage = damage;
        }
    }
}
