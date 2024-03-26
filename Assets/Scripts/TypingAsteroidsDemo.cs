using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TypingAsteroidsDemo : MonoBehaviour
{
    #region spawn rates
    [SerializeField] private float spawnRate;
    #endregion
    #region speeds
    [SerializeField] private float startingSpeed;
    [SerializeField] private float speedIncreaseAdditive;
    [SerializeField] private float speedIncreaseMultiplicative;
    #endregion
    #region wordSettings
    [SerializeField] private string[] words;
    #endregion
    #region typingBox
    [SerializeField] private string typingColor;
    [SerializeField] private TMP_Text textbox;
    #endregion
    
    private ArrayList<TMP_Text> snowflakes;
    
    private float
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
