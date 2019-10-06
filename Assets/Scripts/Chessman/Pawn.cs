﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c, c2;
        int[] e = BoardManager.Instance.EnPassantMove;
        //Light Team
        if (LitOrDark)
        {
            //Diagonal Left
            if(CurrentX > 0 && CurrentY < 7)
            {
                
                if(e[0] == CurrentX-1 && e[1] == CurrentY + 1)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
                c = BoardManager.Instance.Chessmen[CurrentX - 1, CurrentY + 1];
                if(c != null && !c.LitOrDark)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            //Diagonal Right
            if (CurrentX < 7 && CurrentY < 7)
            {
                if (e[0] == CurrentX + 1 && e[1] == CurrentY + 1)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
                c = BoardManager.Instance.Chessmen[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.LitOrDark)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            //MIddle
            if(CurrentY < 7)
            {
                c = BoardManager.Instance.Chessmen[CurrentX, CurrentY + 1];
                if (c == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }
            //MIddle on first move
            if (CurrentY == 1)
            {
                c = BoardManager.Instance.Chessmen[CurrentX, CurrentY + 1];
                c2 = BoardManager.Instance.Chessmen[CurrentX, CurrentY + 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY + 2] = true;
                }
                
            }
        }
        //Dark Team
        else
        {
            //Diagonal Left
            if (CurrentX > 0 && CurrentY > 0)
            {
                if (e[0] == CurrentX - 1 && e[1] == CurrentY - 1)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
                c = BoardManager.Instance.Chessmen[CurrentX - 1, CurrentY - 1];
                if (c != null && c.LitOrDark)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }

            //Diagonal Right
            if (CurrentX < 7 && CurrentY > 0)
            {
                if (e[0] == CurrentX + 1 && e[1] == CurrentY - 1)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
                c = BoardManager.Instance.Chessmen[CurrentX + 1, CurrentY - 1];
                if (c != null && c.LitOrDark)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }
            //MIddle
            if (CurrentY > 0)
            {
                c = BoardManager.Instance.Chessmen[CurrentX, CurrentY - 1];
                if (c == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }
            //MIddle on first move
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.Chessmen[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.Chessmen[CurrentX, CurrentY - 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY - 2] = true;
                }

            }
        }

        return r;
    }
}
