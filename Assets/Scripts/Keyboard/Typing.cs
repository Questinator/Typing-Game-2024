using System;
using System.Text;
using KeyboardEvents;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Keyboard
{
    public class Typing : MonoBehaviour
    {
        [SerializeField] private string typingTarget;
        [SerializeField] private Color unTypedColor;
        [SerializeField] private Color typedCorrectlyColor;
        [SerializeField] private Color incorrectlyTypedColor;
        [SerializeField] private Color cursorColor;

        private float lastCursorBlink;
        
        string typedCorrectlyColorString;
        string untypedColorString;
        string incorrectlyTypedColorString;

        
        private float startTime;
        private float stopTime;
        
        private TextMeshProUGUI text;
        private IKeyboard keyboard;
        private string data;
        private bool delayStartTiming;

        private string cachedText;
        
        
        private int correctKeyPressCount;
        public bool IsRunning {  get; private set; }


        public Typing(IKeyboard keyboard)
        {
            this.keyboard = keyboard;
        }

        private void Awake()
        {
            startTime = -1;
            stopTime = -1;
            
            text = this.GameObject().GetComponent<TextMeshProUGUI>();
            text.SetText(typingTarget);
            keyboard = new KeyboardEvents.Keyboard(0.2,0.05);
            data = "";
            correctKeyPressCount = 0;
        
            typedCorrectlyColorString = Util.TMPHex(typedCorrectlyColor);
            untypedColorString = Util.TMPHex(unTypedColor);
            incorrectlyTypedColorString = Util.TMPHex(incorrectlyTypedColor);
        
        }

        private void Start()
        {
            cachedText = CalculateTextData();
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
                    if (startTime <= -1 && delayStartTiming)
                    {
                        StartTiming();
                    }
                }
                cachedText = CalculateTextData();
            }


            RenderTextData();


        }

        private void RenderTextData()
        {
            text.SetText(cachedText.Replace("\u1111",$"<color=#{Util.TMPHex(cursorColor)}>|</color>"));
        }


        private string CalculateTextData()
        {
            StringBuilder result = new StringBuilder("");
            result.Append($"<color=#{typedCorrectlyColorString}>");
            bool isCorrectSection = true;
            bool isWaitingForSpace = false;
            int timesWaitedForSpace = 0;
            int additionalEndChars = 0;
            correctKeyPressCount = 0;
            
            for (int i = 0; i < data.Length; i++)
            {
                char expectedCharacter;
                if (i - timesWaitedForSpace < typingTarget.Length)
                {
                    expectedCharacter = typingTarget[i - timesWaitedForSpace];
                }
                else
                {
                    additionalEndChars++;
                    continue;
                }
                char typedCharacter = data[i];
                bool isCorrectCharacter = expectedCharacter == typedCharacter;


                if (isCorrectSection)
                {
                    if (isCorrectCharacter)
                    {
                        result.Append(typedCharacter);
                        correctKeyPressCount++;
                    }
                    else
                    {
                        isCorrectSection = false;
                        isWaitingForSpace = SwitchToIncorrectChunk(result, expectedCharacter, typedCharacter, ref timesWaitedForSpace);
                    }
                }
                else if (isWaitingForSpace)
                {
                    if (typedCharacter == ' ')
                    {
                        isCorrectSection = true;
                        correctKeyPressCount++;
                        isWaitingForSpace = false;
                        SwitchToCorrectChunk(result, typedCharacter);
                    }
                    else
                    {
                        AddHangingCharacter(result, typedCharacter);
                        timesWaitedForSpace++;
                    }
                }
                else // Incorrect section - Not waiting for space
                {
                    if (isCorrectCharacter)
                    {
                        correctKeyPressCount++;
                        isCorrectSection = true;
                        SwitchToCorrectChunk(result, typedCharacter);
                    }
                    else
                    {
                        isWaitingForSpace = HandleIncorrectCharacter(result, expectedCharacter, typedCharacter, ref timesWaitedForSpace);
                    }
                }
            }
            result.Append("</color>");
            result.Append("\u1111"); // This will represent our cursor
            result.Append($"<color=#{untypedColorString}>");
            for (int i = data.Length - timesWaitedForSpace; i < typingTarget.Length; i++)
            {
                result.Append(typingTarget[i]);
            }
            result.Append("</color>");
            
            result.Append($"<color=#{incorrectlyTypedColorString}>");
            for (int i = data.Length - additionalEndChars; i < data.Length; i++)
            {
                result.Append(data[i]);
            }
            result.Append("</color>");

            string textData = result.ToString();
            return textData;
        }

        private bool SwitchToIncorrectChunk(StringBuilder result, char expectedCharacter, char typedCharacter,
            ref int timesWaitedForSpace)
        {
            result.Append("</color>");
            result.Append($"<color=#{incorrectlyTypedColorString}>");
            return HandleIncorrectCharacter(result, expectedCharacter, typedCharacter, ref timesWaitedForSpace);
        }

        private bool HandleIncorrectCharacter(StringBuilder result, char expectedCharacter, char typedCharacter,
            ref int timesWaitedForSpace)
        {
            bool isWaitingForSpace = ' ' == expectedCharacter;
            result.Append(!isWaitingForSpace ? expectedCharacter : typedCharacter);
            if (isWaitingForSpace)
            {
                timesWaitedForSpace++;
            }

            return isWaitingForSpace;
        }

        private static void AddHangingCharacter(StringBuilder result, char typedCharacter)
        {
            result.Append(typedCharacter);
        }

        private void SwitchToCorrectChunk(StringBuilder result, char typedCharacter)
        {
            result.Append("</color>");
            result.Append($"<color=#{typedCorrectlyColorString}>");
            result.Append(typedCharacter);
        }
        
        
        
        public void StartTiming()
        {
            startTime = Time.time;
            IsRunning = true;
        }

        public void StopTiming()
        {
            stopTime = Time.time;
            IsRunning = false;
        }

        public void StartTimingOnFirstKeyPress()
        {
            delayStartTiming = true;
        }
        
        
        public double GetRawWpm()
        {
            if (startTime <= -1)
            {
                return -1;
            }
            else if (!IsRunning)
            {
                return (data.Length / 5.0) / ((stopTime - startTime) / 60.0);
            }
            else
            {
                return (data.Length / 5.0) / ((Time.time - startTime) / 60.0);
            }
        }

        public int GetTotalCharacters()
        {
            return typingTarget.Length;
        }

        public int GetTotalTypedCharacters()
        {
            return data.Length;
        }

        public double GetAccuracy()
        {
            if (data.Length == 0)
                return 1;
            return correctKeyPressCount / (double)data.Length;
        }
        
    }
}
