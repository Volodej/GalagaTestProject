using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TopPlayers
{
    public class TopPlayersStorage
    {
        private const string DATA_KEY = "TopPlayerData";
        private const int MINIMAL_SCORE = 2000;

        private readonly SortedList<int, TopPlayerData> _topPlayers;

        public int LowestScore => Math.Max(_topPlayers.Take(10).LastOrDefault().Key, MINIMAL_SCORE);
        public int HighestScore => Math.Max(_topPlayers.FirstOrDefault().Key, MINIMAL_SCORE);
        public List<TopPlayerData> Top10Players => _topPlayers.Select(pair => pair.Value).Take(10).ToList();

        public TopPlayersStorage()
        {
            _topPlayers = LoadData();
        }

        /// <summary>
        /// Add player's score to global rating
        /// </summary>
        /// <returns>Returns player position in the rating.</returns>
        public int AddPlayerScore(string userName, int playerScore)
        {
            var playerData = new TopPlayerData {Name = userName, Score = playerScore};
            _topPlayers.Add(playerScore, playerData);
            SaveData(_topPlayers);
            return _topPlayers.IndexOfValue(playerData);
        }

        private static SortedList<int, TopPlayerData> LoadData()
        {
            var comparer = Comparer<int>.Create((left, right) => left > right ? -1 : 1); // No 0 because of key duplication
            try
            {
                var list = new SortedList<int, TopPlayerData>(comparer);
                var dataString = PlayerPrefs.GetString(DATA_KEY);
                foreach (var data in JsonUtility.FromJson<ListWrapper>(dataString)                    .List)
                {
                    list.Add(data.Score, data);
                }

                return list;
            }
            catch (Exception ex)
            {
                return new SortedList<int, TopPlayerData>(comparer);
            }
        }

        private static void SaveData(SortedList<int, TopPlayerData> data)
        {
            var wrappedData = new ListWrapper {List = data.Select(pair => pair.Value).ToList()};
            var dataString = JsonUtility.ToJson(wrappedData);
            PlayerPrefs.SetString(DATA_KEY, dataString);
            PlayerPrefs.Save();
        }
        
        [Serializable]
        private class ListWrapper
        {
            public List<TopPlayerData> List;
        }
    }
}