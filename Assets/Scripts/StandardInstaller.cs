using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Zenject;

public class StandardInstaller : MonoInstaller
{
    [SerializeField] private PlayerState playerStateInstance;
    [SerializeField] private FirstPersonController fpControllerInstance;
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private AudioSource elevatorAudioSource;

    public override void InstallBindings()
    {
        Container.Bind<PlayerState>().FromInstance(playerStateInstance).AsSingle();
        Container.Bind<FirstPersonController>().FromInstance(fpControllerInstance).AsSingle();
        Container.Bind<ItemManager>().FromInstance(itemManager).AsSingle();

        Container.Bind<ElevatorDoorController>().FromComponentInChildren().AsTransient().WhenInjectedInto<ElevatorController>();
        Container.Bind<ElevatorController>().FromComponentInParents().AsTransient();
        Container.Bind<ElevatorSounds>().FromComponentSibling().AsTransient().WhenInjectedInto<ElevatorController>();
        Container.Bind<ElevatorSounds>().FromComponentsInParents().AsTransient().WhenInjectedInto(typeof(Door), typeof(DoorButton), typeof(ElevatorButtonsPanel));

        Container.Bind<Animator>().FromComponentsInChildren().AsTransient().WhenInjectedInto<ElevatorDoorController>();
        Container.Bind<AudioSource>().FromInstance(elevatorAudioSource).AsTransient().WhenInjectedInto<ElevatorSounds>();
        Container.Bind<AudioSource>().FromComponentSibling().AsTransient();
    }
}