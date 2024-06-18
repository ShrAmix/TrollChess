using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference=null;

    public bool kingRock=false;
    private GameObject[] rock = new GameObject[2];
    private GameObject king;

    //Board position
    int matrixX, matrixY;

    // false: movement, true: attacking
    public bool attack = false;

    private void Start()
    {
        if(attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f,0.0f,0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        reference.GetComponent<ChessMan>().status.firstMove++;
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name=="whiteKing")
            {
                controller.GetComponent<Game>().Winner("black");
            }
            if (cp.name == "blackKing")
            {
                controller.GetComponent<Game>().Winner("white");
            }

            Destroy(cp);
        }
        if (kingRock)
        {
            GameObject r;
            int mov = -2;
            if (controller.GetComponent<Game>().GetCurrentPalyer() == "white")
            {
                GameObject firstWhiteRock = GameObject.Find("whiteRock");
                rock[0] = firstWhiteRock;
                if (firstWhiteRock != null)
                {
                    firstWhiteRock.SetActive(false);
                    GameObject secondWhiteRock = GameObject.Find("whiteRock");
                    firstWhiteRock.SetActive(true);
                    rock[1] = secondWhiteRock;

                    king = GameObject.Find("whiteKing");
                }
            }
            else if (controller.GetComponent<Game>().GetCurrentPalyer() == "black")
            {
                GameObject firstWhiteRock = GameObject.Find("blackRock");
                rock[0] = firstWhiteRock;
                if (firstWhiteRock != null)
                {
                    firstWhiteRock.SetActive(false);
                    GameObject secondWhiteRock = GameObject.Find("blackRock");
                    firstWhiteRock.SetActive(true);
                    rock[1] = secondWhiteRock;

                    king = GameObject.Find("blackKing");
                }
            }
                if (king.transform.position.x < transform.position.x)
                {
                    r = rock[1];
                    mov = -1;
                }
                else
                {
                    r = rock[0];
                    mov = 1;
                }

                if (r != null)
                {
                    r.GetComponent<ChessMan>().SetXBoard(matrixX + mov);
                    r.GetComponent<ChessMan>().SetYBoard(matrixY);
                    r.GetComponent<ChessMan>().SetCoords();
                }



                kingRock = false;
            
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<ChessMan>().GetXBoard(),
            reference.GetComponent<ChessMan>().GetYBoard());
        
        reference.GetComponent<ChessMan>().SetXBoard(matrixX);
        reference.GetComponent<ChessMan>().SetYBoard(matrixY);
        reference.GetComponent<ChessMan>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<ChessMan>().DestroyMovePlantes();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetReference(GameObject obj)
    {
        reference= obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
