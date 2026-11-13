using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Chessman : MonoBehaviour
{

    public GameObject controller;
    public GameObject movePlate;

    //positions
    private int xBoard = -1;
    private int yBoard = -1;

    private string player;



    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");        
    }

    public void setCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.31f;
        y += -2.31f;

        this.transform.position = new Vector3(x, y, 1.5f);
    }

}





