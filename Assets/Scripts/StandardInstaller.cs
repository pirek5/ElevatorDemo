using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Zenject;

public class StandardInstaller : MonoInstaller
{
    [SerializeField] private InputPlayerActions inputInstance;
    [SerializeField] private FirstPersonController fpControllerInstance;
    [SerializeField] private ItemManager itemManager;

    public override void InstallBindings()
    {
        Container.Bind<InputPlayerActions>().FromInstance(inputInstance).AsSingle();
        Container.Bind<DoorController>().FromComponentInParents().AsSingle();
        Container.Bind<FirstPersonController>().FromInstance(fpControllerInstance).AsSingle();
        Container.Bind<ItemManager>().FromInstance(itemManager).AsSingle();
    }
}