using System;
using System.Collections;
using System.Collections.Generic;
using Katniss;
using UnityEngine;
using UnityEngine.UI;

namespace Mizu
{
    public class LevelsUI : MonoBehaviour
    {
        public enum LevelStages
        {
            Dummy,
            level1 = 1,
            level2,
            level3,
        }

        [SerializeField] private Player player;

        [SerializeField] private GameObject dummy;
        [SerializeField] private GameObject level1;
        [SerializeField] private GameObject level2;
        [SerializeField] private GameObject level3;

        private int[] includeCounts;

        private Dictionary<LevelStages, GameObject> levelDictionary = new Dictionary<LevelStages, GameObject>();

        public Action<int> includeCountAction;
        public LevelStages NowStage { get; private set; } = LevelStages.Dummy;
        private static int stage = 0;

        private void Start()
        {
            includeCounts = new[] {0, 4, 5, 7};

            OnInitialize();
            //StartCoroutine(OnSetStage());
            ChangeLevel(1);
        }

        private void OnInitialize()
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);

            levelDictionary.Add(LevelStages.Dummy, dummy);
            levelDictionary.Add(LevelStages.level1, level1);
            levelDictionary.Add(LevelStages.level2, level2);
            levelDictionary.Add(LevelStages.level3, level3);
        }

        private IEnumerator OnSetStage()
        {
            if (stage == 0)
                stage = 1;

            NowStage = (LevelStages)stage;
            levelDictionary.TryGetValue((LevelStages)stage, out var obj);
            obj?.SetActive(true);

            yield return new WaitForSeconds(1f);

            ChangeLevel(stage);
            yield return null;
        }

        public void ChangeLevel(int newLevel)
        {
            if ((int)NowStage == newLevel) return;

            levelDictionary.TryGetValue(NowStage, out var obj);
            obj?.SetActive(false);

            Debug.Log(stage);
            NowStage = (LevelStages)newLevel;
            stage = (int)NowStage;
            levelDictionary.TryGetValue(NowStage, out obj);
            obj?.SetActive(true);
            includeCountAction?.Invoke(includeCounts[(int) NowStage]);
            Debug.Log($"Set include : {includeCounts[(int) NowStage]}");
        }

        public void RetryGame()
        {
            levelDictionary.TryGetValue(NowStage, out var obj);
            obj.SetActive(false);
            obj.SetActive(true);
        }
    }
}
