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

        protected override void OnInstall()
        {
            RegisterInstance(_sessionMaster);
            RegisterInstance(_cameraController);

            RegisterInstruction<UnitDrawController>(true);
            RegisterInstruction<ClickHandler>();
            RegisterInstruction<MonoToEntitySingletonTransferSystem>();

            RegisterMonoComponentInstruction<InputController>();

            World.DefaultGameObjectInjectionWorld.EntityManager.CreateSingleton(
                new LayerSettingsData(0)
                );
        }
    }
}
