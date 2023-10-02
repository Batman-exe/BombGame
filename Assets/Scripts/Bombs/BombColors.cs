using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombColors : Bomb
{
    protected override void EnablePuzzle()
    {
        if (puzzlePrefab != null && playerInTrigger)
        {
            Vector3 position = new Vector3(227.33f, 143.07f, 0);
            GameObject puzzleInstance = Instantiate(puzzlePrefab, position, Quaternion.identity);
            puzzleInstance.GetComponent<ColorSequence>().SetBomb(this);
            playerInTrigger = false;
        }
    }
}
