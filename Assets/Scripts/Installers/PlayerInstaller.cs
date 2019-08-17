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

#if UNITY_EDITOR || UNITY_WEBGL
            if (_enableKeyboardInputInEditor)
                Container.Bind<IUserInput>().To<KeyboardInput>().FromNewComponentOnNewGameObject().AsSingle();
            else
#endif
                Container.Bind<IUserInput>().To<JoystickInput>().AsSingle();
            
            Container.BindMemoryPool<Projectile, Projectile.Pool>()
                .WithId(Identifiers.PlayerProjectile)
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_playerProjectile)
                .UnderTransformGroup("Projectiles");
            
            
            
            Container.BindMemoryPool<Explosion, Explosion.Pool>()
                .WithId(Identifiers.PlayerExplosion)
                .WithInitialSize(1)
                .FromComponentInNewPrefab(_playerExplosion)
                .UnderTransformGroup("Explosions");
            
        }
    }
}