using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katniss
{
    public class Ready : IState
    {
        private Player player;

        public void OnEnter(Player _player)
        {
            player = _player;
        }

        public void OnExit()
        {
            player.SetState(new Drawing());
        }

        public IEnumerator Update()
        {
            yield return null;
        }
    }
}