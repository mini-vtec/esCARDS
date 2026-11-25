using UnityEngine;
using UnityEngine.UI;
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

    void Start()
    {
        game = FindFirstObjectByType<Game>();
        cells = FindFirstObjectByType<cells>();

        ComprobarYAsignarCarta(game.pesa_1, carta_1.image);
        ComprobarYAsignarCarta(game.pesa_2, carta_2.image);
        ComprobarYAsignarCarta(game.pesa_3, carta_3.image);
    }

    void ComprobarYAsignarCarta(GameObject pesa, Image imagenCarta)
    {
        if (pesa == null)
        {
            return;
        }

        Chessman cm = pesa.GetComponent<Chessman>();
        if (cm != null && cm.datosPesa != null && cm.datosPesa.iconoCarta != null)
        {
            imagenCarta.sprite = cm.datosPesa.iconoCarta;
        }
    }

    public void OnCartaClick(int carta_t)
    {
        GameObject pesa_t = null;

        if (carta_t == 1) pesa_t = game.pesa_1;
        if (carta_t == 2) pesa_t = game.pesa_2;
        if (carta_t == 3) pesa_t = game.pesa_3;

        if (pesa_t == null)
        {
            return;
        }

        // Busca una casilla libre en las filas 0 y 1
        bool hayCasilla = game.BuscarCasillaLibre(1);
        if (hayCasilla == false)
        {
            Debug.Log("No hay casillas libres");
            return;
        }

        int x = game.libreX;
        int y = game.libreY;

        Vector3 centreExacte = cells.ObtenirCentreCasella(x, y);
        GameObject nuevaPesa = Instantiate(pesa_t, centreExacte, Quaternion.identity);

        Chessman cm = nuevaPesa.GetComponent<Chessman>();
        cm.Activate(x, y);
        game.SetPos(nuevaPesa, x, y);

        ui.SetActive(false);
    }
}
