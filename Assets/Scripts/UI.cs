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

        if (pesa_t != null)
        {
            int randX = Random.Range(0, 8);
            int randY = Random.Range(0, 2);

            Vector3 centreExacte = cells.ObtenirCentreCasella(randX, randY);
            GameObject nuevaPesa = Instantiate(pesa_t, centreExacte, Quaternion.identity);

            Chessman cm = nuevaPesa.GetComponent<Chessman>();
            // Le pasamos las coordenadas y el color directamente a Activate:
            cm.Activate(randX, randY, "white");
            game.SetPos(nuevaPesa, randX, randY);
        }

        ui.SetActive(false);
    }
}