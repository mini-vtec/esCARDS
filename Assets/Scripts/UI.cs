using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject ui;
    public GameObject panel;
    public Button carta_1;
    public Button carta_2;
    public Button carta_3;
    public Text ronda;

    public Game game;
    public cells cells;

    public Sprite cavall;
    public Sprite alfil;
    public int carta_triada = 0;

    // Start se llama una vez antes de la primera ejecución de Update
    void Start()
    {
        game = FindFirstObjectByType<Game>();
        cells = FindFirstObjectByType<cells>();

        ComprobarYAsignarCarta(game.pesa_1, carta_1.image);
        ComprobarYAsignarCarta(game.pesa_2, carta_2.image);
        ComprobarYAsignarCarta(game.pesa_3, carta_3.image);
    }

    void Update()
    {

    }

    void ComprobarYAsignarCarta(GameObject pesa, UnityEngine.UI.Image imagenCarta)
    {
        if (pesa.name.Contains("white_knight"))
        {
            imagenCarta.sprite = cavall;
        }
        if (pesa.name.Contains("white_bishop"))
        {
            imagenCarta.sprite = alfil;
        }
    }

   public void OnCartaClick(int carta_t)
    {
        GameObject pesa_t = null;

       if (carta_t == 1) pesa_t = game.pesa_1;
       if (carta_t == 2) pesa_t = game.pesa_2;
       if (carta_t == 3) pesa_t = game.pesa_3;

        if (pesa_t.name.Contains("white_bishop"))
        {
            Vector3 centreExacte = cells.ObtenirCentreCasella(Random.Range(0, 7), Random.Range(0, 2));
            Instantiate(game.white_pieces[0], centreExacte, Quaternion.identity);

        }

        if (pesa_t.name.Contains("white_king"))
        {
            Vector3 centreExacte = cells.ObtenirCentreCasella(Random.Range(0, 7), Random.Range(0, 2));
            Instantiate(game.white_pieces[1], centreExacte, Quaternion.identity);
        }

        if (pesa_t.name.Contains("white_knight"))
        {
            Vector3 centreExacte = cells.ObtenirCentreCasella(Random.Range(0, 7), Random.Range(0, 2));
            Instantiate(game.white_pieces[2], centreExacte, Quaternion.identity);
        }

        if (pesa_t.name.Contains("white_pawn"))
        {
            Vector3 centreExacte = cells.ObtenirCentreCasella(Random.Range(0, 7), Random.Range(0, 2));
            Instantiate(game.white_pieces[3], centreExacte, Quaternion.identity);
        }

        if (pesa_t.name.Contains("white_queen"))
        {
            Vector3 centreExacte = cells.ObtenirCentreCasella(Random.Range(0, 7), Random.Range(0, 2));
            Instantiate(game.white_pieces[4], centreExacte, Quaternion.identity);
        }

        if (pesa_t.name.Contains("white_tower"))
        {
            Vector3 centreExacte = cells.ObtenirCentreCasella(Random.Range(0, 7), Random.Range(0, 2));
            Instantiate(game.white_pieces[5], centreExacte, Quaternion.identity);
        }

        ui.SetActive(false);
    }
}