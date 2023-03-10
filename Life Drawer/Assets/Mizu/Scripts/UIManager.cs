using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Katniss;

namespace Mizu
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private LevelsUI _levelUI;
        public LevelsUI LevelsUI => _levelUI;

        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _retryButton;

        [SerializeField] private GameObject _successObj;
        [SerializeField] private GameObject _successVFX;
        [SerializeField] private GameObject _successBackLight;
        [SerializeField] private GameObject _nextObj;

        [SerializeField] private GameObject _normalEye;
        [SerializeField] private GameObject _dieEye;

        [SerializeField] private GameObject _guideLine;
        [SerializeField] private Image startingBG;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
                _guideLine.SetActive(false);
        }

        private void Initialize()
        {
            _nextButton.onClick.AddListener(OnNextButton);
            _retryButton.onClick.AddListener(OnRetryButton);

            _nextObj.SetActive(false);
            _successObj.SetActive(false);
            _successVFX.SetActive(false);

            startingBG.color = Color.clear;
        }

        private void OnNextButton()
        {
            _levelUI.ChangeLevel((int)_levelUI.NowStage + 1);
            _player.SetState(new Starting());
            StartCoroutine(RestartAfterSec());
        }

        private void OnRetryButton()
        {
            _levelUI.RetryGame();
            _player.SetState(new Starting());
            StartCoroutine(RestartAfterSec());
        }

        public void SetSuccess()
        {
            Debug.Log("Success!");
            _successVFX.SetActive(true);
            _successObj.SetActive(true);

            StartCoroutine(backLightRotEffect());
        }

        public void SetFailed()
        {
            Debug.Log("Failed!");
            _normalEye.SetActive(false);
            _dieEye.SetActive(true);

            startingBG.color = Color.black;
            _player.SetState(new Starting());

            StartCoroutine(RestartAfterSec());
        }

        private IEnumerator RestartAfterSec()
        {
            yield return new WaitForSeconds(1.2f);
            SceneManager.LoadScene(0);
        }

        IEnumerator backLightRotEffect()
        {
            var speed = 30f;

            for (var time = 0f; time < 3f; time += Time.deltaTime)
            {
                _successBackLight.transform.Rotate(0, 0, speed * Time.deltaTime);

                yield return null;
            }

            _nextObj.SetActive(true);
        }
    }
}