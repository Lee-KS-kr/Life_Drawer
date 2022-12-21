using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private GameObject level1;
        [SerializeField] private GameObject level2;
        [SerializeField] private GameObject level3;

        private Dictionary<LevelStages, GameObject> levelDictionary = new Dictionary<LevelStages, GameObject>();

        public LevelStages NowStage { get; private set; } = LevelStages.Dummy;

        private void Start()
        {
            OnInitialize();
            ChangeLevel(1);
        }

        private void OnInitialize()
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);

            levelDictionary.Add(LevelStages.level1, level1);
            levelDictionary.Add(LevelStages.level2, level2);
            levelDictionary.Add(LevelStages.level3, level3);
        }

        public void ChangeLevel(int newLevel)
        {
            if ((int)NowStage == newLevel) return;

            levelDictionary.TryGetValue(NowStage, out var obj);
            obj?.SetActive(false);

            NowStage = (LevelStages)newLevel;
            levelDictionary.TryGetValue(NowStage, out obj);
            obj?.SetActive(true);
        }

        public void RetryGame()
        {
            levelDictionary.TryGetValue(NowStage, out var obj);
            obj.SetActive(false);
            obj.SetActive(true);
        }
    }
}