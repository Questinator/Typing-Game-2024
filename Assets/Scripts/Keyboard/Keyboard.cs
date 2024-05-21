using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KeyboardEvents
{

    
    
    public class Keyboard : IKeyboard
    {
        private class KeyData
        {
            public bool IsDown { get; set; } = false;
            public double DepressedTime { get; set; } = 0;
        }
        private readonly Dictionary<KeyCode, KeyData> keyStates;
        
        private readonly Queue<String> keysPressed;
        private readonly IEnumerable<KeyCode> keyCodes;

        private readonly double repeatDelay;
        private readonly double repeatRate;
        public Keyboard(double repeatDelay, double repeatRate)
        {
            this.repeatDelay = repeatDelay;
            this.repeatRate = repeatRate;
            
            keyStates = new Dictionary<KeyCode, KeyData>();
            keyCodes = Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>();
            foreach(KeyCode code in keyCodes)
            {
                keyStates[code] = new KeyData();
            }
            keysPressed = new Queue<String>();
        }

        
        public bool GetKeyState(KeyCode key)
        {
            return keyStates[key].IsDown;
        }
        
        public void Flush()
        {
            bool shiftDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            foreach (KeyCode code in keyCodes)
            {
                keyStates[code].IsDown = Input.GetKey(code);
                if (Input.GetKeyDown(code))
                {
                    keyStates[code].DepressedTime = Time.time;
                    EnqueueKey(code, shiftDown);
                }
                if (keyStates[code].IsDown)
                {
                    if (Time.time - keyStates[code].DepressedTime > repeatDelay + repeatRate)
                    {
                        EnqueueKey(code,shiftDown);
                        keyStates[code].DepressedTime += repeatRate;
                    }
                }

                if (Input.GetKeyUp(code))
                {
                    keyStates[code].DepressedTime = 0;
                }
            }
        }

        private void EnqueueKey(KeyCode code, bool shiftDown)
        {
            String character = KeycodeToString(code);
            if (character is "\b" or "\n")
            {
                keysPressed.Enqueue(character);
            }
            else if (character != null)
            {
                keysPressed.Enqueue(!shiftDown ? character : ApplyShift(character));
            }
        }

        public String GetNextKeyPress()
        {
            return keysPressed.Dequeue();
        }


        public bool HasKeyPress()
        {
            return keysPressed.Count != 0;
        }


        private static String ApplyShift(String character)
        {
            switch (character)
            {
                case "0":
                    return ")";
                case "1":
                    return "!";
                case "2":
                    return "@";
                case "3":
                    return "#";
                case "4":
                    return "$";
                case "5":
                    return "%";
                case "6":
                    return "^";
                case "7":
                    return "&";
                case "8":
                    return "*";
                case "9":
                    return "(";
                case ",":
                    return "<";
                case ".":
                    return ">";
                case "/":
                    return "?";
                case ";":
                    return ":";
                case "/'":
                    return "/";
                default:
                    return character.ToUpper();
            }
        }
        
        private static String KeycodeToString(KeyCode code)
        {
            // This is going to be the worst code I have EVER written
            switch (code)
            {
                case KeyCode.A:
                    return "a";
                case KeyCode.B:
                    return "b";
                case KeyCode.C:
                    return "c";
                case KeyCode.D:
                    return "d";
                case KeyCode.E:
                    return "e";
                case KeyCode.F:
                    return "f";
                case KeyCode.G:
                    return "g";
                case KeyCode.H:
                    return "h";
                case KeyCode.I:
                    return "i";
                case KeyCode.J:
                    return "j";
                case KeyCode.K:
                    return "k";
                case KeyCode.L:
                    return "l";
                case KeyCode.M:
                    return "m";
                case KeyCode.N:
                    return "n";
                case KeyCode.O:
                    return "o";
                case KeyCode.P:
                    return "p";
                case KeyCode.Q:
                    return "q";
                case KeyCode.R:
                    return "r";
                case KeyCode.S :
                    return "s";
                case KeyCode.T:
                    return "t";
                case KeyCode.U :
                    return "u";
                case KeyCode.V:
                    return "v";
                case KeyCode.W:
                    return "w";
                case KeyCode.X:
                    return "x";
                case KeyCode.Y:
                    return "y";
                case KeyCode.Z:
                    return "z";
                case KeyCode.Comma:
                    return ",";
                case KeyCode.Period:
                    return ".";
                case KeyCode.Question:
                    return "?";
                case KeyCode.Quote:
                    return "\'";
                case KeyCode.DoubleQuote:
                    return "\"";
                case KeyCode.Space:
                    return " ";
                case KeyCode.Slash:
                    return "/";
                case KeyCode.Alpha0:
                    return "0";
                case KeyCode.Alpha1:
                    return "1";
                case KeyCode.Alpha2:
                    return "2";
                case KeyCode.Alpha3:
                    return "3";
                case KeyCode.Alpha4:
                    return "4";
                case KeyCode.Alpha5:
                    return "5";
                case KeyCode.Alpha6:
                    return "6";
                case KeyCode.Alpha7:
                    return "7";
                case KeyCode.Alpha8:
                    return "8";
                case KeyCode.Alpha9:
                    return "9";
                case KeyCode.Backspace:
                    return "\b";
                case KeyCode.Return:
                    return "\n";
                default:
                    return null;
            }
        }
    }
    
}