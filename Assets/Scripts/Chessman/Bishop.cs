using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        //Top Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j > 7)
            {
                break;
            }
            c = BoardManager.Instance.Chessmen[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c.LitOrDark != this.LitOrDark)
                    r[i, j] = true;
                break;
            }

        }

        //Top Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i > 7 || j > 7)
            {
                break;
            }
            c = BoardManager.Instance.Chessmen[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c.LitOrDark != this.LitOrDark)
                    r[i, j] = true;
                break;
            }

        }
        //Down Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmen[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c.LitOrDark != this.LitOrDark)
                    r[i, j] = true;
                break;
            }

        }
        //Down Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i > 7 || j < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmen[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c.LitOrDark != this.LitOrDark)
                    r[i, j] = true;
                break;
            }

        }
        return r;
    }
}
