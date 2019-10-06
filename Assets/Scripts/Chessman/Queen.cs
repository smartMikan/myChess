﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i,j;

        //Right
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = BoardManager.Instance.Chessmen[i, CurrentY];
            if (c == null)
            {
                r[i, CurrentY] = true;
            }
            else
            {
                if (c.LitOrDark != this.LitOrDark)
                {
                    r[i, CurrentY] = true;
                }
                break;
            }
        }
        //Left
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = BoardManager.Instance.Chessmen[i, CurrentY];
            if (c == null)
            {
                r[i, CurrentY] = true;
            }
            else
            {
                if (c.LitOrDark != this.LitOrDark)
                {
                    r[i, CurrentY] = true;
                }
                break;
            }
        }
        //Up
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = BoardManager.Instance.Chessmen[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            }
            else
            {
                if (c.LitOrDark != this.LitOrDark)
                {
                    r[CurrentX, i] = true;
                }
                break;
            }
        }
        //Down
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = BoardManager.Instance.Chessmen[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            }
            else
            {
                if (c.LitOrDark != this.LitOrDark)
                {
                    r[CurrentX, i] = true;
                }
                break;
            }
        }
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
