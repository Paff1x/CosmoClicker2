﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Damagable _damagable;

    public static Player Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        _damagable = GetComponent<Damagable>();
    }
    private void Start()
    {
        _damagable.HealthChangeEvent += _damagable.OnHeal;
    }
}
