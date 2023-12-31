using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private BombsConfiguration bombsConfiguration;
    private BombsFactory bombsFactory;
    
    [Tooltip("Places where bombs should appear.")]
    [SerializeField] private Vector3[] spawnPositions;
    private bool[] freePosition;
    private int spawnedBombs;

    private void Awake()
    {
        bombsFactory = new BombsFactory(Instantiate(bombsConfiguration));
        spawnedBombs = 0;
        
        freePosition = new bool[spawnPositions.Length];
        for (int i =0; i < freePosition.Length; i++)
            freePosition[i] = true;
    }
    private void Update()
    {
        // if there's a empty place
        if(spawnPositions.Length > spawnedBombs)
        {
            for (int i = 0; i < freePosition.Length; i++)
            {
                // Finds empty place and spawns a bomb
                if(freePosition[i])
                {
                    SpawnBomb(BombToSpawn(), spawnPositions[i]);
                    freePosition[i] = false;
                    spawnedBombs++;
                }
            }
        }
    }

    /// <summary>
    /// Generates a random bomb id
    /// </summary>
    /// <returns>The id of the random bomb </returns>
    private string BombToSpawn()
    {
        string[] ids = { "QTE", "Colors" };
        int selected = Random.Range(0, ids.Length);
        return ids[selected];
    }

    private void SpawnBomb(string id, Vector3 position)
    {
        bombsFactory.Create(id, position);
    }

    /// <summary>
    /// Sets the position of the defused bomb as free, then waits 5 seconds to respawn a new bomb
    /// </summary>
    /// <param name="position">Defused bomb's position</param>
    public void DeactivateBomb(Vector3 position)
    {
        for(int i = 0; i < spawnPositions.Length; i++)
        {
            if(position == spawnPositions[i])
                freePosition[i] = true;
        }

        StartCoroutine(WaitToRespawn());
    }

    private IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(5f);
        spawnedBombs--;
    }
}
