using System.Collections;
using System.Collections.Generic;
using Keyboard;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;
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
    [SerializeField] private Color typingColor;
    [SerializeField] private TMP_Text textbox;
    #endregion
    
    private List<TMP_Text> snowflakes;

    private float lastSpawnTime = 0;

    private float speed;

    private IKeyboard keyboard;

    private string data;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = new Keyboard.Keyboard(0.1,5);
        speed = startingSpeed;
        data = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSpawnTime >= spawnRate)
        {
            lastSpawnTime = 0;
            SpawnSnowflake();
            speed = speed * speedIncreaseMultiplicative + speedIncreaseAdditive;
        }
        lastSpawnTime += Time.deltaTime;
        
        // Update the keyboard events
        keyboard.Flush();
        // Look through all of the keypresses
        while (keyboard.HasKeyPress())
        {
            string keyData = keyboard.GetNextKeyPress();
            if (keyData == "\b")
            {
                // Delete a character
                if (data.Length > 0)
                {
                    data = data.Remove(data.Length - 1);
                }
            }
            else if (keyData == "\n")
            {
                foreach (TMP_Text snowflake in snowflakes)
                {
                    if (snowflake.text != data) continue;
                    snowflakes.Remove(snowflake);
                    Destroy(snowflake.gameObject);
                    break;
                }
                data = "";
            }
            else
            {
                data += keyData;
            }
            // Render the text
            RenderTextData();
        }
    }

    private void SpawnSnowflake()
    {
        string word = words[Random.Range(0,words.Length)];
        GameObject snowflake = new GameObject("Snowflake ");
        snowflake.transform.parent = this.transform;
        RectTransform rect = (RectTransform)snowflake.transform;
        var parentRect = ((RectTransform)transform).rect;
        rect.position = new Vector2(Random.Range(parentRect.xMin, parentRect.xMax), parentRect.yMax);
    }

    void RenderTextData()
    {
        textbox.SetText(Util.TMPHex(typingColor) + data);
    }
}
