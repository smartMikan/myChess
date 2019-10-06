using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    //TO DO : VFX,UI

    public static BoardManager Instance { get; set; }
    private bool[,] AllowdMoves { get; set; }

    public Chessman[,] Chessmen { set; get; }
    public int[] EnPassantMove { get => enPassantMove; set => enPassantMove = value; }

    private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    public List<GameObject> activeChessman;

    private Color previousColor;
    public Color selectedColor;

    private int[] enPassantMove;

    private Quaternion orient = Quaternion.Euler(0, 180, 0);

    public bool isLightTurn;

    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnAllChessmen();
        isLightTurn = true;
    }

    private void Update()
    {
        DrawChessboard();
        UpdateSelection();

        if (Input.GetMouseButtonDown(0))
        {
            if(selectionX >= 0 && selectionY >= 0)
            {
                if (selectedChessman == null)
                {
                    //select the chessman
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    //move the chessman
                    MoveChessman(selectionX, selectionY);
                }
            }
        }
    }

    private void MoveChessman(int x, int y)
    {
        //selectedChessman.GetComponent<Outline>().OutlineWidth = 0;
        selectedChessman.GetComponent<MeshRenderer>().material.SetColor("Color_6DB210A",previousColor);
        if (AllowdMoves[x, y])
        {
            Chessman c = Chessmen[x, y];
            if(c != null && c.LitOrDark != isLightTurn)
            {
                //Capture a piece

                //If it is the King, End Game
                if (c.GetType() == typeof(King))
                {
                    //End the game
                    EndGame();
                    return;
                }

                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            if (x == EnPassantMove[0] && y == EnPassantMove[1] && selectedChessman.GetType() == typeof(Pawn))
            {
                if (y == 5)
                {
                    activeChessman.Remove(Chessmen[EnPassantMove[0], EnPassantMove[1] - 1].gameObject);
                    Destroy(Chessmen[EnPassantMove[0], EnPassantMove[1] - 1].gameObject);
                }
                else if (y == 2)
                {
                    activeChessman.Remove(Chessmen[EnPassantMove[0], EnPassantMove[1] + 1].gameObject);
                    Destroy(Chessmen[EnPassantMove[0], EnPassantMove[1] + 1].gameObject);
                }
            }
            EnPassantMove[0] = -1;
            EnPassantMove[0] = -1;
            if (selectedChessman.GetType() == typeof(Pawn))
            {
                if(selectedChessman.CurrentY == 1 && y == 3)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y - 1;
                }
                else if (selectedChessman.CurrentY == 6 && y == 4)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y + 1;
                }

                if (y == 7 || y == 0)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    //Promotion
                    SpawnChessman(y == 7 ? 1 : 7, x, y, y == 7);
                    selectedChessman = Chessmen[x, y];
                    
                }
            }

            Chessmen[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
            Chessmen[x, y] = selectedChessman;
            isLightTurn = !isLightTurn;
        }
        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null;
    }

    IEnumerator MoveAnim()
    {
        yield return new WaitForEndOfFrame();
    }

    private void SelectChessman(int x,int y)
    {
        if(Chessmen[x,y] == null)
            return;
        if (Chessmen[x, y].LitOrDark != isLightTurn)
            return;

        bool hasAtLeastOneMove = false;
        AllowdMoves = Chessmen[x, y].PossibleMove();

        foreach (var t in AllowdMoves)
        {
            if (t == true)
            {
                hasAtLeastOneMove = true;
            }
        }
        if (!hasAtLeastOneMove)
        {
            return;
        }

        selectedChessman = Chessmen[x, y];

        previousColor = selectedChessman.GetComponent<MeshRenderer>().material.GetColor("Color_6DB210A");
        selectedChessman.GetComponent<MeshRenderer>().material.SetColor("Color_6DB210A", selectedColor);
        
        BoardHighlights.Instance.HighlightAllowedMoves(AllowdMoves);
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 25f, LayerMask.GetMask("ChessPlane")))
        {
            //Debug.Log(hit.point);
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnChessman(int index, int x, int y, bool LitOrDark)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), LitOrDark ? Quaternion.identity : orient, transform);
        Chessmen[x, y] = go.GetComponent<Chessman>();
        Chessmen[x, y].SetPosition(x, y);
        Chessmen[x, y].LitOrDark = LitOrDark;
        activeChessman.Add(go);
    }
    
    private void SpawnAllChessmen()
    {
        activeChessman = new List<GameObject>();
        Chessmen = new Chessman[8, 8];
        enPassantMove = new int[2] { -1, -1 };
        //Spawn the light team

        //King
        SpawnChessman(0, 3, 0, true);

        //Queen
        SpawnChessman(1, 4, 0, true);
        //Rooks
        SpawnChessman(2, 0, 0, true);
        SpawnChessman(2, 7, 0, true);
        //Bishops
        SpawnChessman(3, 2, 0, true);
        SpawnChessman(3, 5, 0, true);
        //Knights
        SpawnChessman(4, 1, 0, true);
        SpawnChessman(4, 6, 0, true);
        //Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(5,i, 1, true);
        }

        //Spawn the dark team

        //King
        SpawnChessman(6, 4, 7, false);

        //Queen
        SpawnChessman(7,3, 7, false);
        //Rooks
        SpawnChessman(8, 0, 7, false);
        SpawnChessman(8, 7, 7, false);
        //Bishops
        SpawnChessman(9, 2, 7, false);
        SpawnChessman(9, 5, 7, false);
        //Knights
        SpawnChessman(10, 1, 7, false);
        SpawnChessman(10, 6, 7, false);
        //Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, i, 6, false);
        }

    }

    private Vector3 GetTileCenter(int x,int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;

        return origin;
    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start,start+widthLine);
            for (int j = 0; j <= 8 ; j++)
            {
                start = Vector3.right * i;
                Debug.DrawLine(start, start + heightLine);
            }
        }

        //Draw the Selection
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1)
                );
            Debug.DrawLine(
                Vector3.forward * (selectionY + 1)+ Vector3.right * selectionX,
                Vector3.forward * selectionY +Vector3.right * (selectionX + 1)
                );
        }
    }



private void EndGame()
    {
        if (isLightTurn)
        {
            Debug.Log("Light Team Wins!");
        }
        else
        {
            Debug.Log("Dark Team Wins!");
        }
        foreach (GameObject go in activeChessman)
        {
            Destroy(go);
        }

        isLightTurn = true;
        BoardHighlights.Instance.HideHighlights();
        SpawnAllChessmen();
    }
}
