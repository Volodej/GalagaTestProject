using TMPro;
using UIElements.CounterElements;
using UnityEngine;

namespace UIElements
{
    public class HudPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerScoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private SimpleCounterElement _livesCounter;
        [SerializeField] private LevelCounterElement _levelsCounter;

        public void SetPlayerScore(int score) => _playerScoreText.text = score.ToString();
        public void SetHighScore(int score) => _highScoreText.text = score.ToString();
        public void SetPlayerLivesCount(int livesCount) => _livesCounter.SetCount(livesCount);
        public void SetLevelsCount(int levelsCount) => _levelsCounter.SetCount(levelsCount);
    }
}