using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    protected Player Player;

    public void Initialize(Player player) 
    {
        this.Player = player;
    }
}
