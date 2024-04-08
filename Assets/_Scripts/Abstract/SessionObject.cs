using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;

namespace ZE.IsohECS {
	public abstract class SessionObject : MonoBehaviour, ISessionObject
	{
        protected bool GameSessionActive => _sessionStarted & !_isPaused;
		protected bool _sessionStarted = false, _isPaused =false;

        virtual public void OnSessionEnd()
        {
            _sessionStarted = false;
        }
        virtual public void OnSessionStart()
        {
            _sessionStarted = true;
        }
        virtual public void OnPause()
        {
            _isPaused = true;
        }
        virtual public void OnUnpause()
        {
            _isPaused = false;
        }

        private void Awake()
        {
            ServiceLocatorObject.Get<SessionMaster>().SubscribeToSessionEvents(this);
            OnAwake();
        }
        virtual protected void OnAwake() { }
        private void Start()
        {
            OnStart();
        }
        virtual protected void OnStart() { }

        private void OnDestroy()
        {
            if (ServiceLocatorObject.TryGet<SessionMaster>(out var sessionMaster)) sessionMaster.UnsubscribeFromSessionEvents(this);
        }
    }
}
