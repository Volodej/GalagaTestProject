using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "EnemiesInstaller", menuName = "Installers/EnemiesInstaller")]
    public class EnemiesInstaller : ScriptableObjectInstaller<EnemiesInstaller>
    {
        public override void InstallBindings()
        {
        }
    }
}