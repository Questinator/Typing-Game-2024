using Unity.VisualScripting;
using UnityEngine;

namespace Keyboard
{
    public class Util
    {
        public static string TMPHex(Color color)
        {
            return color.ToHexString().TrimEnd(new[] {'0', '0'});
        }
    }
}