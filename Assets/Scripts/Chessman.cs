using UnityEngine;
using System.Collections.Generic;

public class Chessman : MonoBehaviour
{
    public stats datosPesa;
    public GameObject movePlate;

    private Game controller;
    private cells tableroCells;

    private int xBoard = -1;
    private int yBoard = -1;
    private string player = "white";

    public void Activate(int startX, int startY, string playerColor)
    {
        controller = FindFirstObjectByType<Game>();
        tableroCells = FindFirstObjectByType<cells>();

        player = playerColor;
        SetCoords(startX, startY);
    }

    public void SetCoords(int x, int y)
    {
        xBoard = x;
        yBoard = y;

        this.transform.position = tableroCells.ObtenirCentreCasella(xBoard, yBoard);
    }

    public int GetXBoard() { return xBoard; }
    public int GetYBoard() { return yBoard; }
    public string GetPlayer() { return player; }

    private void OnMouseUp()
    {
        if (controller != null && !controller.IsGameOver() && player == "white" && controller.GetCurrentPlayer() == "white")
        {
            DestroyMovePlates();
            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        foreach (Moviment mov in datosPesa.movimentsPermesos)
        {
            for (int paso = 1; paso <= mov.distanciaMaxima; paso++)
            {
                int targetX = xBoard + (mov.direccio.x * paso);
                int targetY = yBoard + (mov.direccio.y * paso);

                // Ajustado a tu nombre PosOnBoard de Game.cs
                if (!controller.PosOnBoard(targetX, targetY))
                    break;

                // Ajustado a tu nombre GetPos de Game.cs
                GameObject piezaEnCasilla = controller.GetPos(targetX, targetY);

                if (piezaEnCasilla == null)
                {
                    MovePlateSpawn(targetX, targetY);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        Vector3 pos = tableroCells.ObtenirCentreCasella(matrixX, matrixY);

        GameObject mp = Instantiate(movePlate, pos, Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}