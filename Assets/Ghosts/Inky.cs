﻿using Assets;
using Assets.Ghosts.ChaseStrategies;
using UnityEngine;

public class Inky : Ghost
{
    public Inky()
    {
        DeadTimer = 3f;
        ScatterTimer = 24;

        chaseStrategies.Add(new InkyStrategy());
    }
}
