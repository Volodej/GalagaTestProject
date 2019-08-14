using System;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    public class EnterNamePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private Button _confirmButton;

        private IObservable<Unit> _nameConfirmed;

        private void Awake()
        {
            _nameConfirmed = _confirmButton.OnClickAsObservable();
        }

        public void SetupPanel(int score)
        {
            _scoreText.text = $"YOUR SCORE: {score}";
            _nameField.text = string.Empty;
        }

        public Task<string> GetUserName()
        {
            var tcs = new TaskCompletionSource<string>();
            _nameConfirmed.Take(1).Subscribe(_ => tcs.SetResult(_nameField.text));
            return tcs.Task;
        } 
    }
}
