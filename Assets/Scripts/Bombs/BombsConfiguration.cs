using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Bomb configuration")]
public class BombsConfiguration : ScriptableObject
{
    [SerializeField] private Bomb[] bombsPrefabs;
    private Dictionary<string, Bomb> idToBomb;

    private void Awake()
    {
        idToBomb = new Dictionary<string, Bomb>(bombsPrefabs.Length);

        foreach (var bomb in bombsPrefabs)
            idToBomb.Add(bomb.Id, bomb);
    }

    public Bomb GetBombPrefabById(string id)
    {
        if (!idToBomb.TryGetValue(id, out var bomb))
        {
            throw new Exception($"Bomb with id {id} does not exit");
        }
        return bomb;
    }
}
