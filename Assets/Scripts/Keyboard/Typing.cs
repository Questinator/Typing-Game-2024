using System;
using System.Text;
using KeyboardEvents;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Typing : MonoBehaviour
{
    [SerializeField] private string typingTarget;
    [SerializeField] private Color unTypedColor;
    [SerializeField] private Color typedCorrectlyColor;
    [SerializeField] private Color incorrectlyTypedColor;
    
    string typedCorrectlyColorString;
    string untypedColorString;
    string incorrectlyTypedColorString;
    
    
    private TextMeshProUGUI text;
    private IKeyboard keyboard;
    private string data;

    public Typing(IKeyboard keyboard)
    {
        this.keyboard = keyboard;
    }

    private void Awake()
    {
        text = this.GameObject().GetComponent<TextMeshProUGUI>();
        text.SetText(typingTarget);
        keyboard = new Keyboard(0.2,0.05);
        data = "";
        
        typedCorrectlyColorString = typedCorrectlyColor.ToHexString().TrimEnd(new []{'0','0'});
        untypedColorString = unTypedColor.ToHexString().Trim(new []{'0','0'});
        incorrectlyTypedColorString = incorrectlyTypedColor.ToHexString().Trim(new []{'0','0'});
        
    }
    // Update is called once per frame
    void Update()
    {
        keyboard.Flush();
        while (keyboard.HasKeyPress())
        {
            String keyData = keyboard.GetNextKeyPress();
            if (keyData == "\b")
            {
                if (data.Length > 0)
                    data = data.Remove(data.Length - 1);
            }
            else
            {
                data += keyData;
            }
        }

        StringBuilder result = new StringBuilder("");
        result.Append($"<color=#{typedCorrectlyColorString}>");
        bool isCorrectSection = true;
        bool isWaitingForSpace = false;
        int timesWaitedForSpace = 0;
        for (int i = 0; i < data.Length; i++)
        {
            char expectedCharacter = typingTarget[i - timesWaitedForSpace];
            bool isCorrectCharacter = expectedCharacter == data[i];


            if (isCorrectSection && !isWaitingForSpace)
            {
                if (isCorrectCharacter)
                {
                    result.Append(data[i]);
                }
                else
                {
                    isCorrectSection = false;
                    result.Append("</color>");
                    result.Append($"<color=#{incorrectlyTypedColorString}>");
                    
                    isWaitingForSpace = ' ' == typingTarget[i];
                    result.Append(!isWaitingForSpace ? expectedCharacter : data[i]);
                    if (isWaitingForSpace)
                    {
                        timesWaitedForSpace++;
                    }
                }
            }
            else if (isWaitingForSpace)
            {
                if (data[i] == ' ')
                {
                    isCorrectSection = true;
                    result.Append("</color>");
                    result.Append($"<color=#{typedCorrectlyColorString}>");
                    result.Append(data[i]);
                    isWaitingForSpace = false;

                }
                else
                {
                    result.Append(data[i]);
                    timesWaitedForSpace++;
                }
            }
            else // Incorrect section - Not waiting for space
            {
                if (isCorrectCharacter)
                {
                    isCorrectSection = true;

                    result.Append("</color>");
                    result.Append($"<color=#{typedCorrectlyColorString}>");
                    result.Append(data[i]);
                }
                else
                {
                    isWaitingForSpace = ' ' == typingTarget[i];
                    result.Append(!isWaitingForSpace ? expectedCharacter : data[i]);
                    if (isWaitingForSpace)
                    {
                        timesWaitedForSpace++;
                    }
                }
            }
        }

        result.Append("</color>");

        result.Append($"<color=#{untypedColorString}>");
        for (int i = data.Length - timesWaitedForSpace; i < typingTarget.Length; i++)
        {
            result.Append(typingTarget[i]);
        }
        result.Append("</color>");

        text.SetText(result.ToString());

        
        
    }
}
