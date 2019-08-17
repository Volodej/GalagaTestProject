using System.Collections.Generic;
using System.Linq;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

[CreateAssetMenu(fileName = "LevelsOrderInstaller", menuName = "Installers/LevelsOrderInstaller")]
public class LevelsOrderInstaller : ScriptableObjectInstaller<LevelsOrderInstaller>
{
    [SerializeField] private List<SceneReference> _scenes;
    [SerializeField] private bool _takeOnlyFirstScene;
    
    public override void InstallBindings()
    {
        var scenesToTake = _takeOnlyFirstScene ? _scenes.Take(1).ToList() : _scenes;
        Container.BindInstance(new ScenesOrder(scenesToTake)).AsSingle();
    }
}