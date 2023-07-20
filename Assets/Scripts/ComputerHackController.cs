using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public enum GlyphType
{
    Regular,
    Activatable,
    Active
}
[Serializable]
public class Glyph
{
    public string text;
    public GlyphType type = GlyphType.Regular;
}

public class GlyphUnition
{
    List<Glyph> _glyphs;

    public int Length
    {
        get
        {
            return _glyphs.Sum(g => g.text.Length);
        }
    }
    public GlyphUnition(List<Glyph> glyphs)
    {
        _glyphs = glyphs;
    }

    public string Substring(int start, int len)
    {
        var currentLength = 0;
        var offset = 0;
        var glyphIndex = 0;
        var res = "";
        var resLength = 0;
        foreach (var glyph in _glyphs)
        {
            if (currentLength + glyph.text.Length >= start)
            {
                offset = start - currentLength;
                break;
            }
            glyphIndex++;
            currentLength += glyph.text.Length;
        }
        for (var i = glyphIndex; i < _glyphs.Count; i++)
        {
            var length = len - resLength;
            if (length + offset < _glyphs[i].text.Length)
            {
                var sub = _glyphs[i].text.Substring(offset, length);
                if (_glyphs[i].type == GlyphType.Active)
                {
                    sub = $"<u>{sub}</u>";
                }
                return res + sub;
            }
            else
            {
                var sub = _glyphs[i].text.Substring(offset);
                resLength += sub.Length;
                if (_glyphs[i].type == GlyphType.Active)
                {
                    sub = $"<u>{sub}</u>";
                }
                res += sub;
            }
            offset = 0;
        }
        return res;
    }

    public string Substring(int start)
    {
        var currentLength = 0;
        var offset = 0;
        var glyphIndex = 0;
        var res = "";
        foreach (var glyph in _glyphs)
        {
            if (currentLength + glyph.text.Length >= start)
            {
                offset = currentLength + glyph.text.Length - start;
            }
            glyphIndex++;
            currentLength += glyph.text.Length;
        }
        for (var i = glyphIndex; i < _glyphs.Count; i++)
        {
            var sub = _glyphs[i].text.Substring(offset);
            if (_glyphs[i].type == GlyphType.Active)
            {
                sub = $"<u>{sub}</u>";
            }
            res += sub;
            offset = 0;
        }
        return res;
    }
}
public class ComputerHackController : MonoBehaviour
{
    public TMP_Text leftColumn;
    public TMP_Text rightColumn;
    public TMP_Text console;

    public int rowBase = 61623;
    public int columnHeight = 17;
    public int rowWidth = 13;
    GlyphUnition glyphUnition;
    public int wordLength = 5;
    public List<Glyph> words = new List<Glyph>();
    public List<Glyph> activatable = new List<Glyph>();
    string consoleText = "";
    int wordIndex = 0;
    Glyph currentWord
    {
        get
        {
            return activatable[wordIndex];
        }
    }
    List<string> textRows
    {
        get
        {
            var rows = new List<string>();
            for (int i = 0; i < glyphUnition.Length; i += rowWidth)
            {
                if (glyphUnition.Length > i + rowWidth)
                {
                    rows.Add(glyphUnition.Substring(i, rowWidth));
                }
                else
                {
                    rows.Add(glyphUnition.Substring(i));
                }
            }
            return rows;
        }
    }

    string getTextColumnString(List<string> textRows, int start, int end, int extLength = 0)
    {
        var res = "";
        var len = rowBase + extLength;
        for (var i = start; i < end; i++)
        {

            var row = textRows[i];
            res += $"0X{len.ToString("X")} {row}\n";
            len += rowWidth;
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
            return getTextColumnString(textRows, columnHeight, columnHeight * 2, columnHeight * rowWidth);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        glyphUnition = new GlyphUnition(words);
        foreach (var glyph in words)
        {
            if (glyph.type == GlyphType.Activatable)
            {
                activatable.Add(glyph);
            }
        }
        activatable[0].type = GlyphType.Active;
        consoleText = activatable[0].text;
    }

    void Update()
    {
        leftColumn.text = leftText;
        rightColumn.text = rightText;
        console.text = $">{consoleText}";
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentWord.type = GlyphType.Activatable;
            wordIndex = (wordIndex + 1) % activatable.Count;
            currentWord.type = GlyphType.Active;
            consoleText = currentWord.text;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentWord.type = GlyphType.Activatable;
            wordIndex = wordIndex - 1;
            if (wordIndex < 0)
            {
                wordIndex = activatable.Count - 1;
            }
            currentWord.type = GlyphType.Active;
            consoleText = currentWord.text;
        }
    }
}
