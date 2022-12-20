using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drawing : IState
{
    private Player player;

    public void OnEnter(Player _player)
    {
        player = _player;
    }

    public void OnExit()
    {
        player.SetState(new Waiting());
    }

    public IEnumerator Update()
    {
        yield return null;
    }
}
