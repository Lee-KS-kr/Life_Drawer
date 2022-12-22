using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katniss
{
    public class Starting : IState
    {
        private Player player;
        private Color color;

        [SerializeField] private CanvasRenderer fadeInOutCanvas;

        public void OnEnter(Player _player)
        {
            player = _player;
            fadeInOutCanvas = GameObject.Find("StartingBG").GetComponent<CanvasRenderer>();
        }

        public void OnExit()
        {
        }

        public IEnumerator Update()
        {
            var fadeInOutTime = 1.2f;

            fadeInOutCanvas.SetColor(Color.black);
            Debug.Log("turn on");
            color = fadeInOutCanvas.GetColor();

            for (var time = 0f; time < fadeInOutTime * 2; time += Time.deltaTime)
            {
                if (time < fadeInOutTime)
                {
                    color.a = time / fadeInOutTime;
                    fadeInOutCanvas.SetColor(color);
                    yield return null;
                }
                else
                {
                    color.a = (fadeInOutTime * 2 - time) / fadeInOutTime;
                    fadeInOutCanvas.SetColor(color);
                    yield return null;
                }
            }

            fadeInOutCanvas.SetColor(Color.clear);
            Debug.Log("turn off");
        }
    }
}