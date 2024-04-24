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


    public static string TimeToString(float _secs)
    {
        float secsNum = _secs;

        int mins = Mathf.FloorToInt(secsNum / 60);
        string minsStr = mins.ToString();
        if (minsStr.Length < 1)
            minsStr = "0" + minsStr;

        int secs = Mathf.FloorToInt(secsNum - mins * 60);
        string secsStr = secs.ToString();
        if (secsStr.Length < 2)
            secsStr = "0" + secsStr;

        int ms = Mathf.FloorToInt((secsNum % 1) * 1000);
        string msStr = ms.ToString();
        while (msStr.Length < 3)
            msStr = "0" + msStr;

        return minsStr + ":" + secsStr + ":" + msStr;
    }
}
