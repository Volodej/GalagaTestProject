using UnityEngine;

namespace LifeCycle.Game
{
    public class GameSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _nextLevel;
        [SerializeField] private AudioClip _noRecord;
        [SerializeField] private AudioClip _newRecord;
        [SerializeField] private AudioClip _fighterDestroyed;

        public void PlayNextLevel()
        {
            _audioSource.clip = _nextLevel;
            _audioSource.Play();
        }
        public void PlayNoRecord()
        {
            _audioSource.clip = _noRecord;
            _audioSource.Play();
        }
        public void PlayNewRecord()
        {
            _audioSource.clip = _newRecord;
            _audioSource.Play();
        }
        public void PlayFighterDestroyed()
        {
            _audioSource.clip = _fighterDestroyed;
            _audioSource.Play();
        }
    }
}