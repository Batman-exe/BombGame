using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsFactory
{
    private readonly BombsConfiguration bombsConfiguration;

    public BombsFactory(BombsConfiguration bombsConfiguration)
    {
        this.bombsConfiguration = bombsConfiguration;
    }

    public Bomb Create(string id, Vector3 position)
    {
        var prefab = bombsConfiguration.GetBombPrefabById(id);
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }
}
