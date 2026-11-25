using UnityEngine;
using System.Collections.Generic;

public class Chessman : MonoBehaviour
{
    public stats datosPesa;
    public bool esJugador = true;

    // Vida de ESTA ficha. datosPesa.vida es solo la vida maxima.
    public int vidaActual;

    private Game controller;
    private cells tableroCells;

    private int xBoard = -1;
    private int yBoard = -1;

    public void Activate(int startX, int startY, bool esDelJugador = true)
    {
        controller = FindFirstObjectByType<Game>();
        tableroCells = FindFirstObjectByType<cells>();

        esJugador = esDelJugador;
        vidaActual = datosPesa.vida;
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

    public void RecibirDano(int cantidad)
    {
        vidaActual = vidaActual - cantidad;

        if (vidaActual <= 0)
        {
            controller.SetPosEmpty(xBoard, yBoard);
            DestroyMovePlates();
            Destroy(gameObject);
        }
    }

    private void OnMouseUp()
    {
        if (controller != null && !controller.IsGameOver() && esJugador && controller.GetCurrentPlayer() == "white")
        {
            DestroyMovePlates();
            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        MovePlate[] movePlates = FindObjectsByType<MovePlate>(FindObjectsSortMode.None);
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i].gameObject);
        }
    }

    public void InitiateMovePlates()
    {
        // Las fichas enemigas van hacia el otro lado, asi que se invierte la Y
        int signo = 1;
        if (esJugador == false)
        {
            signo = -1;
        }

        foreach (Moviment mov in datosPesa.movimentsPermesos)
        {
            // Los movimientos solo de ataque se usaran mas adelante, con el combate
            if (mov.soloAtaque == true)
            {
                continue;
            }

            for (int paso = 1; paso <= mov.distanciaMaxima; paso++)
            {
                int targetX = xBoard + (mov.direccio.x * paso);
                int targetY = yBoard + (mov.direccio.y * signo * paso);

                // Si la casilla no esta libre (fuera del tablero, obstaculo o pieza), se corta
                if (controller.IsFree(targetX, targetY) == false)
                {
                    break;
                }

                MovePlateSpawn(targetX, targetY);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        Vector3 pos = tableroCells.ObtenirCentreCasella(matrixX, matrixY);
        pos.y += 0.1f;

        GameObject mp = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        mp.transform.position = pos;
        mp.transform.localScale = new Vector3(2.5f, 0.05f, 2.5f);
        mp.GetComponent<MeshRenderer>().material.color = Color.gray;

        MovePlate mpScript = mp.AddComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
