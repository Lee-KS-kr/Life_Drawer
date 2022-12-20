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
        }

        public void OnExit()
        {
            player.SetState(new Ready());
        }

        public IEnumerator Update()
        {
            var fadeInOutTime = 0.5f;

            fadeInOutCanvas.gameObject.SetActive(true);
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
                    color.a = (fadeInOutTime - time) / fadeInOutTime;
                    fadeInOutCanvas.SetColor(color);
                    yield return null;
                }
            }

            fadeInOutCanvas.gameObject.SetActive(false);
            OnExit();
        }
    }
}