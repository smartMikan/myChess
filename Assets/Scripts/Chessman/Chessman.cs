using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }

    public bool LitOrDark;

    public void SetPosition(int x,int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMove()
    {
        return new bool[8,8];
    }
}
