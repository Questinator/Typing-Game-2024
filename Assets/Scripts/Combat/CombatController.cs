using System;
using System.Collections.Generic;

public class CombatController
{
    private Random rand;
    
    /// <summary>
    /// All entities in this combat encounter.
    /// </summary>
    private List<CombatEntity> entities;
    /// <summary>
    /// All entities in this combat encounter.
    /// </summary>
    public List<CombatEntity> Entities => entities;
    
    /// <summary>
    /// Whose turn it currently is. (This is 0 based)
    /// </summary>
    private int turn;

    public CombatController(List<CombatEntity> entities)
    {
        this.entities = entities;

        turn = 0;
        rand = new Random();
    }
    
    /// <summary>
    /// Casts a random spell from what the attacker can cast.
    /// </summary>
    public Spell GetRandomSpell()
    {
        CombatEntity entity = GetCurrentEntity();
        return entity.Spells[rand.Next(0, entity.Spells.Length)];
    }

    /// <summary>
    /// Gets the spell with the relevant position in the attacker's <c>spells</c> array.
    /// </summary>
    /// <param name="spellNum">The position on the attacker's <c>spells</c> array for the spell.</param>
    public Spell GetSpell(int spellNum)
    {
        CombatEntity entity = GetCurrentEntity();
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
    /// <param naame="target">The target of the spell.</param>
    /// <returns>The result of combat.</returns>
    private SpellResult CastSpell(Spell spell, int accuracy, int speed, CombatEntity target)
    {
        CombatEntity entity = GetCurrentEntity();
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
        target.Damage(damage);
        SwitchTurn();
        return new SpellResult(spell, damage);
    }
    
    /// <summary>
    /// Casts a spell. (debug method, for only 2 CombatEntities)
    /// </summary>
    /// <param name="spell">Which spell to cast.</param>
    /// <param name="accuracy">The accuracy of the typing.</param>
    /// <param name="speed">The speed of the typing.</param>
    /// <returns>The result of combat.</returns>
    private SpellResult CastSpell(Spell spell, int accuracy, int speed, CombatEntity target)
    {
        CombatEntity entity = GetCurrentEntity();
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
        target.Damage(damage);
        SwitchTurn();
        return new SpellResult(spell, damage);
    }
    
    /// <summary>
    /// Return the result of combat from an AI.
    /// </summary>
    /// <returns>The result of combat.</returns>
    public SpellResult DoAITurn()
    {
        CombatEntity entity = GetCurrentEntity();
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
    
    /// <summary>
    /// Does the player's turn.
    /// </summary>
    /// <param name="spell">The spell the player is casting.</param>
    /// <param name="accuracy">The accuracy the player typed the incantation.</param>
    /// <param name="speed">The speed the player typed the incantation.</param>
    /// <returns>The result of combat.</returns>
    public SpellResult DoPlayerTurn(Spell spell, int accuracy, int speed)
    {
        return CastSpell(spell, accuracy, speed);
    }
    
    /// <summary>
    /// Gets the CombatEntity whose turn it is.
    /// </summary>
    /// <returns>The CombatEntity whose turn it is.</returns>
    private CombatEntity GetCurrentEntity()
    {
        return entities[turn];
    }
    
    /// <summary>
    /// Switches the current turn.
    /// </summary>
    private void SwitchTurn()
    {
        turn++;
        if (turn >= entities.Count)
        {
            turn = 0;
        }
    }
    
    /// <summary>
    /// The result of combat.
    /// </summary>
    public struct SpellResult
    {
        public Spell spell;
        public int damage;

        public SpellResult(Spell spell, int damage)
        {
            this.spell = spell;
            this.damage = damage;
        }
    }
}
