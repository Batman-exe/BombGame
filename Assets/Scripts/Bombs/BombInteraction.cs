using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BombInteraction : Bomb
{
    private bool playerInTrigger = false;
    [SerializeField] private GameObject puzzlePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerInTrigger = false;
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
            EnablePuzzle();
    }

    private void EnablePuzzle()
    {
        if (puzzlePrefab != null && playerInTrigger)
        {
            Instantiate(puzzlePrefab);
            playerInTrigger = false;
        }
    }
}
