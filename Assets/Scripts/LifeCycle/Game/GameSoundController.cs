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
        [SerializeField] private AudioClip _attackWave;

        public void PlayNextLevel() => PlayClip(_nextLevel);
        public void PlayNoRecord() => PlayClip(_noRecord);
        public void PlayNewRecord() => PlayClip(_newRecord);
        public void PlayFighterDestroyed() => PlayClip(_fighterDestroyed);
        public void PlayAttackWave() => PlayClip(_attackWave);

        private void PlayClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.time = 0;
            _audioSource.Play();
        }
    }
}