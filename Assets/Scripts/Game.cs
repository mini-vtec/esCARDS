using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public GameObject pesa_1;
    public GameObject pesa_2;
    public GameObject pesa_3;

    public GameObject[] black_pieces;
    public GameObject[] white_pieces;

    // Obstaculos
    public GameObject obstaclePrefab;   // opcional: si no s'ha assignat, es crea un cub gris
    public int numObstacles = 6;       // maxim 10

    // Aqui es guarda el resultat de BuscarCasillaLibre
    public int libreX;
    public int libreY;

    private GameObject[,] positions = new GameObject[8, 8]; //guarda les posicions de les peces al tauler
    private bool[,] obstacles = new bool[8, 8]; //guarda les posicions dels obstacles al tauler

    private string CurrentPlayer = "white";
    private bool gameOver = false;

    void Awake()
    {
        Seleccio_peces();
        ColocarObstacle();
    }
// tria 3 peces aleatories de white_pieces i les assigna a pesa_1 pesa_2 i pesa_3, un cop tria la peça la borra de la llista
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

        pesa_2 = dispo_pieces[index];
        dispo_pieces.RemoveAt(index);

        pesa_3 = dispo_pieces[index];
        dispo_pieces.RemoveAt(index);
    }

    //coloca obstacles aleatories al tauler, entre les files 2 i 5, fins a un maxim de numObstacles
    void ColocarObstacle()
    {
        if (numObstacles > 10)
        {
            numObstacles = 10;
        }

        int colocats = 0;

        while (colocats < numObstacles)
        {
            int x = Random.Range(1, 7);
            int y = Random.Range(1, 7);

            if (obstacles[x, y] == false)
            {
                obstacles[x, y] = true;
                CrearObstacle(x, y);
                colocats++;
            }
        }
    }

    void CrearObstacle(int x, int y)
    {
        cells taulell = FindFirstObjectByType<cells>();
        Vector3 centre = taulell.ObtenirCentreCasella(x, y);

        if (obstaclePrefab != null)
        {
            Instantiate(obstaclePrefab, centre, Quaternion.identity);
        }
        else
        {
            GameObject cub = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cub.transform.position = centre;
            cub.transform.localScale = new Vector3(3f, 1.5f, 3f);
            cub.GetComponent<MeshRenderer>().material.color = Color.gray;
            Destroy(cub.GetComponent<Collider>());
        }
    }

    public bool IsObstacle(int x, int y)
    {
        return obstacles[x, y];
    }

    // analitza les caselles per saber si ni a una de lliure, si esta fora del tauler, si hi ha un obstacle o una peça
    public bool IsFree(int x, int y)
    {
        if (PosOnBoard(x, y) == false) return false;
        if (obstacles[x, y]) return false;
        if (positions[x, y] != null) return false;
        return true;
    }

    // busca una casella lliure a les files 0 i 1, si la troba, retorna true i deixa les coordenades a libreX i libreY
    // Si la encuentra, devuelve true y deja las coordenadas en libreX y libreY.
    public bool BuscarCasillaLibre(int filaMaxima)
    {
        for (int i = 0; i < 100; i++)
        {
            int x = Random.Range(0, 8);
            int y = Random.Range(0, filaMaxima + 1);

            if (IsFree(x, y))
            {
                libreX = x;
                libreY = y;
                return true;
            }
        }

        return false;
    }

    // ---------- Piezas ----------

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

    public string GetCurrentPlayer() { return CurrentPlayer; }
    public bool IsGameOver() { return gameOver; }
}
