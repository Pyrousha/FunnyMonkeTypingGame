using UnityEngine;

public static class Utils
{
    /// <summary>
    /// Converts an int to an ASCII character, starting with 1->A, 2->B, etc.
    /// </summary>
    /// <returns></returns>
    public static char IntToChar_ASCII(int _val)
    {
        int asciiIndex = _val + 64;
        char newChar = (char)asciiIndex;

        return newChar;
    }

    /// <summary>
    /// Converts a char to an int, starting with A->1, B->2, etc.
    /// </summary>
    public static int CharToInt_ASCII(char _char)
    {
        int index = _char;
        return index - 64;
    }

    public static string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }
}
