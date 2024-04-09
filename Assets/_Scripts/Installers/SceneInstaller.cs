using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using Unity.Entities;

namespace ZE.IsohECS {
    public class SceneInstaller : InstallerBase
    {
        [SerializeField] private SessionMaster _sessionMaster;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private GameElementsPack _gameElementsPack;

        protected override void OnInstall()
        {
            RegisterInstance(_sessionMaster);
            RegisterInstance(_cameraController);
            RegisterInstance(_gameElementsPack);

            RegisterInstruction<UnitDrawController>(true);
            RegisterInstruction<ClickHandler>();
            RegisterInstruction<MonoToEntitySingletonTransferSystem>();
            RegisterInstruction<ObjectsSpawnManager>();
            RegisterInstruction<SelectionMarksManager>();

            RegisterMonoComponentInstruction<InputController>();
            RegisterMonoComponentInstruction<VisibleEntitiesSpawnMonoSystem>(true);

            World.DefaultGameObjectInjectionWorld.EntityManager.CreateSingleton(
                new LayerSettingsData(0)
                );
        }
    }
}
