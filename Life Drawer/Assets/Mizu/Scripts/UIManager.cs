using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Katniss;

namespace Mizu
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private LevelsUI _levelUI;

        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _retryButton;

        [SerializeField] private GameObject _failedObj;
        [SerializeField] private GameObject _successObj;
        [SerializeField] private GameObject _successVFX;
        [SerializeField] private GameObject _pen;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _nextButton.onClick.AddListener(OnNextButton);
            _retryButton.onClick.AddListener(OnRetryButton);

            _failedObj.SetActive(false);
            _successObj.SetActive(false);
            _successVFX.SetActive(false);
            _pen.SetActive(false);
        }

        private void OnNextButton()
        {
            _levelUI.ChangeLevel((int)_levelUI.NowStage + 1);
            _player.SetState(new Starting());
        }

        private void OnRetryButton()
        {
            _levelUI.RetryGame();
            _player.SetState(new Starting());
        }

        public void SetSuccess()
        {
            _successVFX.SetActive(true);
            _successObj.SetActive(true);
        }

        public void SetFailed()
        {
            _failedObj.SetActive(false);
            _player.SetState(new Starting());
        }
    }
}