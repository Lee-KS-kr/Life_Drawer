using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Katniss
{
    public class Starting : IState
    {
        private Player player;
        private Color color;

        [SerializeField] private Image fadeInOutCanvas;

        public void OnEnter(Player _player)
        {
            player = _player;
            fadeInOutCanvas = GameObject.Find("StartingBG").GetComponent<Image>();
        }

        public void OnExit()
        {
        }

        public IEnumerator Update()
        {
            var fadeInOutTime = 1.2f;

   
            Debug.Log("turn on");
            color = fadeInOutCanvas.color;

            for (var time = 0f; time < fadeInOutTime * 2; time += Time.deltaTime)
            {
                if (time < fadeInOutTime)
                {
                    color.a = time / fadeInOutTime;
                    fadeInOutCanvas.color = color;
                    yield return null;
                }
                else
                {
                    color.a = (fadeInOutTime * 2 - time) / fadeInOutTime;
                    fadeInOutCanvas.color = color;
                    yield return null;
                }
            }

            fadeInOutCanvas.color = Color.clear;
            Debug.Log("turn off");
        }
    }
}