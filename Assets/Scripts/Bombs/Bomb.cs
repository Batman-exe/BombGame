using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Bomb : MonoBehaviour
{
    [SerializeField] protected string id;
    [SerializeField] protected GameObject puzzlePrefab;
    protected bool playerInTrigger = false;
    protected BombSpawner bombSpawner;
    public string Id => id;

    private void Start()
    {
        bombSpawner = FindObjectOfType<BombSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInTrigger = false;
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
            EnablePuzzle();
    }

    /// <summary>
    /// Destroys the bomb and notifies the bomb spawner.
    /// </summary>
    public void Defuse()
    {
        bombSpawner.DeactivateBomb(transform.position);
        Destroy(gameObject);
    }

    protected abstract void EnablePuzzle();
}
