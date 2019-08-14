using System;
using TMPro;
using TopPlayers;
using UnityEngine;

namespace UIElements
{
    public class TopPlayerItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rankAndNameText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Color _normalColor = new Color(0.9f, 0.9f, 0.9f);
        [SerializeField] private Color _highlightingColor = new Color(0.9f, 0.9f, 0.2f);

        private int _rank = 1;
        private string _name = string.Empty;
        private bool _isHighlighted;

        public void SetData(TopPlayerData data)
        {
            _scoreText.text = data.Score.ToString();
            _name = data.Name;
            UpdateRankAndName();
        }

        public TopPlayerItem SetPosition(int rank)
        {
            _rank = rank;
            UpdateRankAndName();
            return this;
        }

        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                _isHighlighted = value;
                var color = _isHighlighted ? _highlightingColor : _normalColor;
                _rankAndNameText.color = color;
                _scoreText.color = color;
            }
        }

        private void UpdateRankAndName()
        {
            _rankAndNameText.text = $"{_rank} {_name}";
        }
    }
}