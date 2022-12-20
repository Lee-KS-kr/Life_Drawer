using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katniss
{
    public class Success : IState
    {
        private Player player;

        public void OnEnter(Player _player)
        {
            player = _player;
        }

        public void OnExit()
        {
            player.SetState(new Starting());
        }

        public IEnumerator Update()
        {
            yield return null;
        }
    }
}