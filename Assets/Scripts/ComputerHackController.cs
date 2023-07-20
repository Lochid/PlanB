using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerHackController : MonoBehaviour
{
    public TMP_Text leftColumn;
    public TMP_Text rightColumn;
    public TMP_Text console;

    public int rowBase = 61623;
    public int columnHeight = 17;
    public int rowWidth = 13;
    public string text = "";
    List<string> textRows = new List<string>();

    string getTextColumnString(List<string> textRows, int start, int end, int extLength = 0)
    {
        var res = "";
        var len = rowBase + extLength;
        for (var i = start; i < end; i++)
        {
            var row = textRows[i];
            res += "0X" + len.ToString("X") + " " + row + "\n";
            len += row.Length;
        }
        return res;
    }
    string leftText
    {
        get
        {
            return getTextColumnString(textRows, 0, columnHeight);
        }
    }
    string rightText
    {
        get
        {
            return getTextColumnString(textRows, columnHeight, columnHeight*2, columnHeight* rowWidth);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < text.Length; i += rowWidth)
        {
            if (text.Length > i + rowWidth)
            {
                textRows.Add(text.Substring(i, rowWidth));
            }
            else
            {
                textRows.Add(text.Substring(i));
            }
        }
    }

    void Update()
    {
        leftColumn.text = leftText;
        rightColumn.text = rightText;
    }
}
