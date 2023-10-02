using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWire : Bomb
{
    protected override void EnablePuzzle()
    {
        if (puzzlePrefab != null && playerInTrigger)
        {
            Vector3 position = new Vector3(0, 0, 0);
            GameObject puzzleInstance = Instantiate(puzzlePrefab, position, Quaternion.identity);
            puzzleInstance.GetComponent<WireWinLose>().SetBomb(this);

            playerInTrigger = false;
        }
    }
}
