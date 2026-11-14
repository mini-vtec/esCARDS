using UnityEngine;
using System.Collections.Generic;

public class Chessman : MonoBehaviour
{
    public stats datosPesa;
    public MovePlate moveplate;

    [Header("Equipo")]
    public bool esJugador = true; // Marcado (true) para tus piezas, desmarcado (false) para los enemigos

    private Game controller;
    private cells tableroCells;

    private int xBoard = -1;
    private int yBoard = -1;

    public void Activate(int startX, int startY, bool esDelJugador = true)
    {
        controller = FindFirstObjectByType<Game>();
        tableroCells = FindFirstObjectByType<cells>();
        moveplate = FindFirstObjectByType<MovePlate>();

        esJugador = esDelJugador;
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

    private void OnMouseUp()
    {
        // Mucho más limpio: solo comprueba si es tu pieza y si es tu turno
        if (controller != null && !controller.IsGameOver() && esJugador && controller.GetCurrentPlayer() == "white")
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

                if (!controller.PosOnBoard(targetX, targetY))
                    break;

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

        // Como moveplate ya es de tipo MovePlate, Instantiate nos devuelve el script directamente
        MovePlate mpScript = Instantiate(moveplate, pos, Quaternion.identity);

        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}