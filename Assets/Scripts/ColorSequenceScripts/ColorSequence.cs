using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSequence : MonoBehaviour
{
    //lista de los botones
    [SerializeField] Button[] buttons;
    //lista para almacenar el index en orden de los botones que se deben presionar
    [SerializeField] List<int> colorOrder;

    [SerializeField] private float pickDelay = 0.4f;
    private int pickNumber = 0;

    private AudioSource audioSound;
    [SerializeField]private AudioClip good;
    [SerializeField]private AudioClip bad;
    private int mistakes;

    private BombSpawner bombSpawner;

    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject gameOver;


    private void Start()
    {
        bombSpawner = FindObjectOfType<BombSpawner>();
        gameOver = GameObject.Find("GameOverCanvas");

        audioSound = GetComponent<AudioSource>();
        ResetGame();
        SetButtonIndex();
    }

    private void Update()
    {
        if (timer.GetComponent<Timer>().timeIsUp == true)
        {
            gameOver.transform.GetChild(0).gameObject.SetActive(true); 
            Destroy(gameObject, 0.5f);
        }
    }

    //Le asigna un index a cada boton de acuerdo a la posicion en la que se agregaron
    private void SetButtonIndex()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].buttonIndex = i;
        }
    }
    //Corutina de juego principal, hace un ciclo por el arreglo para mostrar que botones deben ser presionados
    IEnumerator PlayGame()
    {
        if(colorOrder.Count >= 6)
        {
            bombSpawner.GetComponent<BombSpawner>().DeactivateBomb();
            Destroy(gameObject, 0.5f);

        }
        else if (mistakes > 1 || timer.GetComponent<Timer>().timeIsUp == true)
        {
            Destroy(gameObject, 0.5f);
            gameOver.transform.GetChild(0).gameObject.SetActive(true);

        }
        pickNumber = 0;
        yield return new WaitForSeconds(pickDelay);
        foreach (int i in colorOrder)
        {
            buttons[i].PressButton();
            yield return new WaitForSeconds(pickDelay);
        }

        PickRandomColor();

    }


    //Funcion para elegir un botton random agregarlo a la lista y resaltarlo
    private void PickRandomColor()
    {
        int randomNum = Random.Range(0, buttons.Length);
        buttons[randomNum].PressButton();
        colorOrder.Add(randomNum);
    }

    //Funcion para verificar que boton eligío el jugador
    public void PlayersPick(int pick)
    {

        if (pick == colorOrder[pickNumber])
        {
            Debug.Log("correct");
            pickNumber++;
            if(pickNumber == colorOrder.Count)
            {
                audioSound.PlayOneShot(good);
                StartCoroutine(PlayGame());
            }
        } else
        {
            audioSound.PlayOneShot(bad);
            mistakes++;
            ResetGame();
        }

    }

    
    private void ResetGame()
    {
        colorOrder.Clear();
        StartCoroutine(PlayGame());
    }

}
