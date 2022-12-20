using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katniss
{
    public class Waiting : IState
    {
        private Player player;

        public void OnEnter(Player _player)
        {
            player = _player;

            //start Beating (Heart)
        }

        public void OnExit()
        {
            //if ()
            //{
            //    player.SetState(new Success());
            //    player.SetState(new Fail());
            //set player state depends on flag
            //}
        }

        public IEnumerator Update()
        {
            yield return null;
        }
    }
}