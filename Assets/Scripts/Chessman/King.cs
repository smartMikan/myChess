using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
    //TO DO :castling
    public bool CanCastling;
    //TO DO :CantGo Area

    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;
        

        //Top Side
        i = CurrentX - 1;
        j = CurrentY + 1;
        if(CurrentY < 7)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Chessmen[i, j];

                    if (c == null)
                    {
                        r[i, j] = true;
                    }
                    else if (c.LitOrDark != this.LitOrDark)
                    {
                        r[i, j] = true;
                    }
                }
                i++;
            }
        }
        //Down Side
        i = CurrentX - 1;
        j = CurrentY - 1;
        if (CurrentY > 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Chessmen[i, j];

                    if (c == null)
                    {
                        r[i, j] = true;
                    }
                    else if (c.LitOrDark != this.LitOrDark)
                    {
                        r[i, j] = true;
                    }
                }
                i++;
            }
        }
        //Left & Right
        if (CurrentX > 0 && CurrentX < 7)
        {
            i = CurrentX - 1;
            j = CurrentY;
            c = BoardManager.Instance.Chessmen[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else if (c.LitOrDark != this.LitOrDark)
            {
                r[i, j] = true;
            }
            i = CurrentX + 1;
            c = BoardManager.Instance.Chessmen[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else if (c.LitOrDark != this.LitOrDark)
            {
                r[i, j] = true;
            }
        }

        //foreach (var chess in BoardManager.Instance.activeChessman)
        //{
        //    if (chess.GetComponent<Chessman>().LitOrDark != this.LitOrDark)
        //    {
        //        for (int l = 0; l < 8; l++)
        //        {
        //            for (int m = 0; m < 8; m++)
        //            {
        //                if (chess.GetComponent<Chessman>().PossibleMove()[l, m] == true)
        //                {
        //                    r[l, m] = false;
        //                }
        //            }
        //        } 
        //    }
        //}


        return r;
    }
}
