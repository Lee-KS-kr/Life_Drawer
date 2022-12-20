using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katniss
{
    public class Fail : IState
    {
        private Player player;

        [SerializeField] GameObject baseFace;
        [SerializeField] GameObject failFace;

        public void OnEnter(Player _player)
        {
            player = _player;

            baseFace.SetActive(false);
            failFace.SetActive(true);
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