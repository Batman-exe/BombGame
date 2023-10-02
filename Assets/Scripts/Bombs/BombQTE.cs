using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombQTE : Bomb
{
    protected override void EnablePuzzle()
    {
        if (puzzlePrefab != null && playerInTrigger)
        {
            Vector3 position = new Vector3(0, 0, -6.0f);
            GameObject puzzleInstance = Instantiate(puzzlePrefab, position, Quaternion.identity);
            puzzleInstance.GetComponent<QTEController>().SetBomb(this);

            playerInTrigger = false;
        }
    }
}
