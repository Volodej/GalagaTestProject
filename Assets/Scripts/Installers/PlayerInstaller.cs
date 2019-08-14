using Units;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "PlayerInstaller", menuName = "Installers/PlayerInstaller")]
    public class PlayerInstaller : ScriptableObjectInstaller<PlayerInstaller>
    {
        [SerializeField] private PlayerShip _playerPrefab;
        
        public override void InstallBindings()
        {
        }
    }
}