using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireWinLose : MonoBehaviour
{
    public int wiresConected;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject gameOver;
    private BombWire bombWire;

    private void Start()
    {
        gameOver = GameObject.Find("GameOverCanvas");
        FindObjectOfType<CharacterMovement>().ableToMove = false;
    }

    private void Awake()
    {
        GameObject[] bomb = GameObject.FindGameObjectsWithTag("Bomb");
        foreach(GameObject booom in bomb)
        {
            booom.GetComponent<CircleCollider2D>().enabled = false;
        }
    }


    private void Update()
    {
        if (wiresConected == 4)
        {
            bombWire.Defuse();
            FindObjectOfType<CharacterMovement>().ableToMove = true;
            GameObject[] bomb = GameObject.FindGameObjectsWithTag("Bomb");
            foreach (GameObject booom in bomb)
            {
                booom.GetComponent<CircleCollider2D>().enabled = true;
            }
            Destroy(parent, 0.5f);
        } else if ( timer.GetComponent<Timer>().timeIsUp == true )
        {
            Destroy(parent, 0.5f);
            gameOver.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void SetBomb(BombWire wireBomb)
    {
        bombWire = wireBomb;
    }

}
