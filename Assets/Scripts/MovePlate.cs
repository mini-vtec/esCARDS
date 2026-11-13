using UnityEngine;

public class MovePlate : MonoBehaviour
{
    private Game controller;
    private GameObject reference = null;

    private int matrixX;
    private int matrixY;

    public void Start()
    {
        controller = FindFirstObjectByType<Game>();
    }

    public void OnMouseUp()
    {
        Chessman cm = reference.GetComponent<Chessman>();

        // 1. Vaciamos la casilla anterior (usando tu nombre SetPosEmpty)
        controller.SetPosEmpty(cm.GetXBoard(), cm.GetYBoard());

        // 2. Movemos la pieza a las nuevas coordenadas
        cm.SetCoords(matrixX, matrixY);

        // 3. Registramos la pieza en la nueva casilla (usando tu nombre SetPos)
        controller.SetPos(reference, matrixX, matrixY);

        // 4. Borramos las placas de movimiento del suelo
        cm.DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }
}