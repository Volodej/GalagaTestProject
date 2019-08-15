using Units;
using UnityEngine;
using UserInput;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "PlayerInstaller", menuName = "Installers/PlayerInstaller")]
    public class PlayerInstaller : ScriptableObjectInstaller<PlayerInstaller>
    {
        [SerializeField] private PlayerShip _playerPrefab;
        [SerializeField] private Explosion _playerExplosion;
        [SerializeField] private Projectile _playerProjectile;
        [SerializeField] private bool _enableKeyboardInputInEditor;

        public override void InstallBindings()
        {
            Container.Bind<PlayerShip>().FromComponentInNewPrefab(_playerPrefab).AsSingle();
            Container.BindInstance(_playerExplosion);

#if UNITY_EDITOR
            if (_enableKeyboardInputInEditor)
                Container.Bind<IUserInput>().To<KeyboardInput>().FromNewComponentOnNewGameObject().AsSingle();
            else
#endif
                Container.Bind<IUserInput>().To<JoystickInput>().AsSingle();
            
            Container.BindMemoryPool<Projectile, Projectile.Pool>()
                .WithId(Identifiers.PlayerProjectile)
                .WithInitialSize(2)
                .FromComponentInNewPrefab(_playerProjectile)
                .UnderTransformGroup("Projectiles");
        }
    }
}