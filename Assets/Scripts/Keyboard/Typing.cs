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
        /// <summary>
        /// Sentence to type
        /// </summary>
        [SerializeField] private string typingTarget;
        /// <summary>
        /// Color of the text if it hasn't been typed
        /// </summary>
        [SerializeField] private Color unTypedColor;
        /// <summary>
        /// Color of the text
        /// </summary>
        [SerializeField] private Color typedCorrectlyColor;
        /// <summary>
        /// Color of the text if it has been typed wrong
        /// </summary>
        [SerializeField] private Color incorrectlyTypedColor;
        /// <summary>
        /// Color of the cursor
        /// </summary>
        [SerializeField] private Color cursorColor;
        /// <summary>
        /// Charachter for the cursor
        /// </summary>
        [SerializeField] private string cursorCharachter;
        
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
        private bool enterPressed;
        private bool ignoreEnter;

        /// <summary>
        /// Is the typing thing active?
        /// </summary>
        public bool IsRunning {  get; private set; }

        /// <summary>
        /// Sentence to type
        /// </summary>
        public string TypingTarget
        {
            get => typingTarget;
            set
            {
                typingTarget = value;
                if (text != null)
                {
                    cachedText = CalculateTextData();
                    text.SetText(cachedText);
                }
            }
        }

        /// <summary>
        /// Setup
        /// </summary>
        private void Awake()
        {
            ignoreEnter = false;
            startTime = -1;
            stopTime = -1;
            
            text = this.GameObject().GetComponent<TextMeshProUGUI>();
            text.SetText(TypingTarget);
            keyboard = new KeyboardEvents.Keyboard(0.2,0.05);
            data = "";
            correctKeyPressCount = 0;
            enterPressed = false;
        
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
            // Update the keyboard events
            keyboard.Flush();
            // Look through all of the keypresses
            while (keyboard.HasKeyPress())
            {
                String keyData = keyboard.GetNextKeyPress();
                if (keyData == "\n")
                {
                    enterPressed = true;
                    if (ignoreEnter)
                    {
                        continue;
                    }
                }
                if (keyData == "\b")
                {
                    // Delete a character
                    if (data.Length > 0)
                        data = data.Remove(data.Length - 1);
                }
                else
                {

                    // Add keydata to the typed words
                    data += keyData;
                    // Start timing if we aren't yet
                    if (startTime <= -1 && delayStartTiming)
                    {
                        StartTiming();
                    }
                }
                // Calculate what we need to show
                cachedText = CalculateTextData();
            }

            // Render the text
            RenderTextData();


        }

        private void RenderTextData()
        {
            text.SetText(cachedText.Replace("\u1111",$"<color=#{Util.TMPHex(cursorColor)}>{cursorCharachter}</color>"));
        }


        private string CalculateTextData()
        {
            // Create the result
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
                if (i - timesWaitedForSpace < TypingTarget.Length)
                {
                    expectedCharacter = TypingTarget[i - timesWaitedForSpace];
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
            for (int i = data.Length - timesWaitedForSpace; i < TypingTarget.Length; i++)
            {
                result.Append(TypingTarget[i]);
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
        
        
        /// <summary>
        /// Start the timer
        /// </summary>
        public void StartTiming()
        {
            startTime = Time.time;
            IsRunning = true;
        }
        /// <summary>
        /// Stop the timer
        /// </summary>
        public void StopTiming()
        {
            stopTime = Time.time;
            IsRunning = false;
        }
        /// <summary>
        /// Start timing once the user presses a key
        /// </summary>
        public void StartTimingOnFirstKeyPress()
        {
            delayStartTiming = true;
        }
        
        /// <summary>
        /// Get the raw wpm typed of all characters since typing started
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Number of characters in the target string
        /// </summary>
        /// <returns></returns>
        public int GetTotalCharacters()
        {
            return TypingTarget.Length;
        }
        /// <summary>
        /// Number of chacters typed
        /// </summary>
        /// <returns></returns>
        public int GetTotalTypedCharacters()
        {
            return data.Length;
        }
        /// <summary>
        /// Gets the perventage of the text that is correctly typed
        /// </summary>
        /// <returns></returns>
        public double GetAccuracy()
        {
            if (data.Length == 0)
                return 1;
            return correctKeyPressCount / (double)data.Length;
        }
        
        public bool IsEnterPressed()
        {
            return enterPressed;
        }

        public void Reset()
        {
            Awake();
        }

        public void SetIgnoreEnter(bool ignore)
        {
            ignoreEnter = ignore;
        }
    }
}
