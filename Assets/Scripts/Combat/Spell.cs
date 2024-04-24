using UnityEngine;

[CreateAssetMenu]
public class Spell : ScriptableObject
{
    [SerializeField]
    private string spellName;
    [SerializeField]
    private string incantation;
    
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float minAccuracy;
    
    [SerializeField]
    private float critSpeedThreshhold;
    [SerializeField]
    private float critAccuracyThreshold;
    [SerializeField] 
    private float critMultiplier = 2f;
    
    [SerializeField]
    private int damage;
    
    /// <summary>
    /// The name of this spell.
    /// </summary>
    public string SpellName => spellName;
    
    /// <summary>
    /// What the player has to type to cast this spell.
    /// </summary>
    public string Incantation => incantation;
    
    /// <summary>
    /// The minimum typing speed required to cast this spell.
    /// </summary>
    public float MinSpeed => minSpeed;

    /// <summary>
    /// The minimum typing accuracy required to cast this spell.
    /// </summary>
    public float MinAccuracy => minAccuracy;
    
    /// <summary>
    /// The threshold of speed the player has to reach to do a critical hit.
    /// </summary>
    public float CritSpeedThreshhold => critSpeedThreshhold;
    
    /// <summary>
    /// The threshold of accuracy the player has to reach to do a critical hit.
    /// </summary>
    public float CritAccuracyThreshold => critAccuracyThreshold;
    
    /// <summary>
    /// How much a crit increases the damage (2x by default).
    /// </summary>
    public float CritMultiplier => critMultiplier;
    
    /// <summary>
    /// The damage this spell does.
    /// </summary>
    public int Damage => damage;
}
