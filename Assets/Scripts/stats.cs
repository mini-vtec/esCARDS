using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Moviment
{
    public Vector2Int direccio;
    public int distanciaMaxima;
}

[CreateAssetMenu(fileName = "stats", menuName = "Scriptable Escacs/stats")]
public class stats : ScriptableObject
{
    public string nom_pesa;
    public Sprite iconoCarta;
    public int vida;
    public int atac;
    public List<Moviment> movimentsPermesos;

}
