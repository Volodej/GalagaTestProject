using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UIElements.CounterElements;
using UnityEngine;

namespace UIElements
{
    public class HudPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerScoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private TMP_Text _stageNumberText;
        [SerializeField] private SimpleCounterElement _livesCounter;
        [SerializeField] private LevelCounterElement _levelsCounter;

        [SerializeField, Range(0.5f, 5)] 
        private float _stageShowingDelay = 2;

        public void SetPlayerScore(int score) => _playerScoreText.text = score.ToString();
        public void SetHighScore(int score) => _highScoreText.text = score.ToString();
        public void SetPlayerLivesCount(int livesCount) => _livesCounter.SetCount(livesCount);
        public void SetLevelsCount(int levelsCount) => _levelsCounter.SetCount(levelsCount);

        public async Task ShowStageNumber(int stageNumber)
        {
            _stageNumberText.gameObject.SetActive(true);
            _stageNumberText.text = $"STAGE  {stageNumber}";
            await Task.Delay(Mathf.RoundToInt(_stageShowingDelay * 1000));
            _stageNumberText.gameObject.SetActive(false);
        }
    }
}