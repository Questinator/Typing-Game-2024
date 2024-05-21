using UnityEngine;

[RequireComponent(typeof(PlayerMovementScript))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// Enable/Disable a cutscene
    /// </summary>
    public bool CutsceneState { get; set; }

    private PlayerMovementScript movementScript;

    private void Awake()
    {
        movementScript = GetComponent<PlayerMovementScript>();
    }
}