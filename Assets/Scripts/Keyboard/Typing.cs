using System;
using KeyboardEvents;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Typing : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private IKeyboard _keyboard;
    private string data = "";
    
    private void Awake()
    {
        _text = this.GameObject().GetComponent<TextMeshProUGUI>();
        Debug.Log(_text);
        _keyboard = new Keyboard(1,0.5);
    }
    // Update is called once per frame
    void Update()
    {
        _keyboard.Flush();
        while (_keyboard.HasKeyPress())
        {
            String keyData = _keyboard.GetNextKeyPress();
            if (keyData == "\b")
            {
                if (data.Length > 0)
                    data = data.Remove(data.Length - 1);
            }
            else
            {
                data += keyData;
            }
            _text.SetText(data);
        }
    }
}
