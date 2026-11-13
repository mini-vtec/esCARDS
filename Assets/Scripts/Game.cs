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

    private GameObject[] playerBlack = new GameObject[1];
    private GameObject[] playerWhite = new GameObject[1];
    private GameObject[,] positions = new GameObject[8, 8];

    private string CurrentPlayer = "white";
    private bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Seleccio_peces();
        Debug.Log(pesa_1.name);
        Debug.Log(pesa_2.name);
        Debug.Log(pesa_3.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Seleccio_peces()
    {
        // Implementar la lógica para seleccionar piezas
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
}
