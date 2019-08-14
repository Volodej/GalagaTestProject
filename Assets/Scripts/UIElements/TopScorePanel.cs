using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopPlayers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    public class TopScorePanel : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private TopPlayerItem _itemPrefab;
        [SerializeField] private RectTransform _contentHolder;

        private IObservable<Unit> _newGameConfirmed;
        private List<TopPlayerItem> _items;

        private void Awake()
        {
            _newGameConfirmed = _newGameButton.OnClickAsObservable();
            _items = CreateItems();
        }
        
        public void SetupPanel(List<TopPlayerData> players, int playersPosition)
        {
            const int totalItemsCount = 10;
            for (int i = 0; i < totalItemsCount; i++)
            {
                var data = players.Count < i ? players[i] : TopPlayerData.Empty;
                _items[i].SetData(data);
                _items[i].IsHighlighted = i + 1 == playersPosition;
            }
        }

        public Task WaitForNewGame()
        {
            var tcs = new TaskCompletionSource<Unit>();
            _newGameConfirmed.Take(1).Subscribe(_ => tcs.SetResult(Unit.Default));
            return tcs.Task;
        }

        private List<TopPlayerItem> CreateItems()
        {
            const int itemsToAdd = 9;
            _itemPrefab.SetPosition(1);
            return Enumerable.Repeat(_itemPrefab, 1)
                .Concat(Enumerable.Range(2, itemsToAdd)
                    .Select(position => Instantiate(_itemPrefab, _contentHolder).SetPosition(position)))
                .ToList();
        }
    }
}