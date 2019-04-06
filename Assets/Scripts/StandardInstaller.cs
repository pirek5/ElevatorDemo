using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Zenject;

public class StandardInstaller : MonoInstaller
{
    [SerializeField] private PlayerState playerStateInstance;
    [SerializeField] private FirstPersonController fpControllerInstance;
    [SerializeField] private ItemManager itemManager;

    public override void InstallBindings()
    {
        Container.Bind<PlayerState>().FromInstance(playerStateInstance).AsSingle();
        Container.Bind<FirstPersonController>().FromInstance(fpControllerInstance).AsSingle();
        Container.Bind<ItemManager>().FromInstance(itemManager).AsSingle();

        Container.Bind<ElevatorDoorController>().FromComponentInChildren().AsTransient().WhenInjectedInto<ElevatorController>();
        Container.Bind<ElevatorController>().FromComponentInParents();

        Container.Bind<Animator>().FromComponentsInChildren().AsTransient().WhenInjectedInto<ElevatorDoorController>();
    }
}