using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerHackController : MonoBehaviour
{
    public TMP_Text leftColumn;
    public TMP_Text rightColumn;
    public TMP_Text console;

    string leftTextRow;
    string leftText;
    string rightText;

    // Start is called before the first frame update
    void Start()
    {
        leftText = "0xF0B7 @\\...........\n" +
        "0xF0C3 :?,:,$!@$!!.&\n" +
        "0xF0CF ,OXBOW#^/?#/,\n" +
        "0xF0DB ^?%,........*\n" +
        "0xF0E7 ,@_@&&/#\\^+!.\n" +
        "0xF0F3 ;=,&,;%,,....\n" +
        "0xF0FF .-%?\\+$/%$&^\\\n" +
        "0xF10B :&\\,|%+/@|!%:\n" +
        "0xF117 =-\\&,_/==,-?=\n" +
        "0xF123 ^.SATIN:-;|^^\n" +
        "0xF12F %,^|,<,;#%*$%\n" +
        "0xF13B ^>;[#$\\.#[;]^\n" +
        "0xF147 (*:\\-];.),@|(\n" +
        "0xF153 ?*TEPID=,.^^?\n" +
        "0xF15F @:(_&,_(+!)+@\n" +
        "0xF16B \\\\@}\\=|RISEN\\\n" +
        "0xF177 ?%<%+/%\\,?>.?\n";
        rightText = "0xF183 \\?$$%&;,++^:\\\n" +
        "0xF18F FOXED;$$&\\??;\n" +
        "0xF19B ,==_$$#^^,,BA\n" +
        "0xF1A7 LLY//-;,,&^@/\n" +
        "0xF1B3 !;=|_@$\\_{=?!\n" +
        "0xF1BF .......(:/#&.\n" +
        "0xF1CB .=/}/-\\^;!*[.\n" +
        "0xF1D7 %@;,*&$],HUBB\n" +
        "0xF1E3 Y^+,?:&^=::#^\n" +
        "0xF1EF GROUP%;.[:@;%\n" +
        "0xF1FB ^@*;]<+&_,,&;\n" +
        "0xF207 |>?|,#\\.++,-|\n" +
        "0xF213 /=+;\\$&/?\\,/R\n" +
        "0xF21F OILS::=^#!;_:\n" +
        "0xF22B %@#?_|!,$;+$%\n" +
        "0xF237 %.....^**?;\\%\n" +
        "0xF243 ^<@-*|,#?>,%^\n";
    }

    void Update()
    {
        leftColumn.text = leftText;
        rightColumn.text = rightText;
    }
}
