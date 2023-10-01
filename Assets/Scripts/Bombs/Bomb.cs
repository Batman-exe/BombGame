using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bomb : MonoBehaviour
{
    [SerializeField] protected string id;

    public string Id => id;
}
