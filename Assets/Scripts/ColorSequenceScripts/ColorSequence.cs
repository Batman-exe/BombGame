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

    private void Start()
    {
        ResetGame();
        SetButtonIndex();
        StartCoroutine(PlayGame());
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

    //Funcion para verificar que boton eligio el jugador
    public void PlayersPick(int pick)
    {
        Debug.Log(pick);

        if (pick == colorOrder[pickNumber])
        {
            Debug.Log("correct");
            pickNumber++;
            if(pickNumber == colorOrder.Count)
            {
                StartCoroutine(PlayGame());
            }
        } else
        {
            Debug.Log("lose");

            ResetGame();
        }

    }

    
    private void ResetGame()
    {
        colorOrder.Clear();
    }

}
