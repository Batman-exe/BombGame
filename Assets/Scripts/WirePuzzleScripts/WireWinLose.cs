using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireWinLose : MonoBehaviour
{
    public int wiresConected;
    [SerializeField] private GameObject parent;
    private BombSpawner bombSpawner;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject gameOver;


    private void Start()
    {
        bombSpawner = FindObjectOfType<BombSpawner>();
        gameOver = GameObject.Find("GameOverCanvas");

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
            bombSpawner.GetComponent<BombSpawner>().DeactivateBomb();
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



}
