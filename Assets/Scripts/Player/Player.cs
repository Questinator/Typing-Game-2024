using UnityEngine;

[RequireComponent(typeof(PlayerMovementScript))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// Enable/Disable a cutscene
    /// </summary>
    public bool CutsceneState
    {
        get
        {
            return cutsceneState;
        }
        set
        {
            cutsceneState = value;

            // Disable the player when in a cutscene
            movementScript.SetCutsceneState(value);
        }
    }
    private bool cutsceneState;

    private PlayerMovementScript movementScript;

    private void Awake()
    {
        movementScript = GetComponent<PlayerMovementScript>();
    }
}