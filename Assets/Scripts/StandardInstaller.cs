using UnityEngine;
using Zenject;

public class StandardInstaller : MonoInstaller
{
    [SerializeField] private InputPlayerActions inputInstance;

    public override void InstallBindings()
    {
        Container.Bind<InputPlayerActions>().FromInstance(inputInstance).AsSingle();
        Container.Bind<DoorController>().FromComponentInParents().AsSingle();
    }
}