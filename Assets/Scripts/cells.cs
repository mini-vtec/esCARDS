using UnityEngine;

public class cells : MonoBehaviour
{
    public Vector3 ObtenirCentreCasella(int casellaX, int casellaY)
    {
        // El punt de partida de la primera casella (0,0) és -13.125
        float posicioX = -13.125f + (casellaX * 3.75f);
        float posicioZ = -13.125f + (casellaY * 3.75f);
        float posicioY = 1;

        return new Vector3(posicioX, posicioY, posicioZ);
    }
}
