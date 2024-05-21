using System;
using UnityEngine;

namespace KeyboardEvents
{
    public interface IKeyboard
    {
        /// <summary>
        /// Returns whether a key is down
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>The state of the key</returns>
        public bool GetKeyState(KeyCode key);
        
        
        /// <summary>
        /// Checks for new key presses and updates key states
        /// </summary>
        public void Flush();

        /// <summary>
        /// Returns the letter repersentation of the last key presesed.
        /// </summary>
        /// <returns>Key that was pressed</returns>
        public String GetNextKeyPress();
        /// <summary>
        /// Returns true if there is a key press waiting to be parsed
        /// </summary>
        /// <returns>whether or not there is a key press to process</returns>
        public bool HasKeyPress();

    }
}