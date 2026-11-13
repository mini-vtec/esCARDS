using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public GameObject pesa_1;
    public GameObject pesa_2;
    public GameObject pesa_3;

    public GameObject[] black_pieces;
    public GameObject[] white_pieces;

    private GameObject[,] positions = new GameObject[8, 8];

    private string CurrentPlayer = "white";
    private bool gameOver = false;

    void Awake()
    {
        Seleccio_peces();
        Debug.Log(pesa_1.name);
        Debug.Log(pesa_2.name);
        Debug.Log(pesa_3.name);
    }

    void Update()
    {

    }

    void Seleccio_peces()
    {
        if (white_pieces == null || white_pieces.Length < 3)
        {
            Debug.LogError("white_pieces debe contener al menos 3 piezas.");
            return;
        }

        List<GameObject> dispo_pieces = new List<GameObject>(white_pieces);

        int index = Random.Range(0, dispo_pieces.Count);
        pesa_1 = dispo_pieces[index];
        dispo_pieces.RemoveAt(index);

        index = Random.Range(0, dispo_pieces.Count);
        pesa_2 = dispo_pieces[index];
        dispo_pieces.RemoveAt(index);

        index = Random.Range(0, dispo_pieces.Count);
        pesa_3 = dispo_pieces[index];
        dispo_pieces.RemoveAt(index);
    }

    public void SetPos(GameObject obj, int x, int y)
    {
        positions[x, y] = obj;
    }

    public void SetPosEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPos(int x, int y)
    {
        return positions[x, y];
    }

    public bool PosOnBoard(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < 8 && y < 8);
    }

    // Faltaban estas dos funciones para que Chessman.cs no dé error:
    public string GetCurrentPlayer() { return CurrentPlayer; }
    public bool IsGameOver() { return gameOver; }
}