using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZE.ServiceLocator;
using System;

namespace ZE.IsohECS {
    public sealed class SessionMaster : MonoBehaviour
    {
        private enum GameState : byte { AwaitForStart, Game, Pause, GameFinished, LoadingNextScene }
        private GameState _gameState = GameState.AwaitForStart;
        public bool SessionStarted => _gameState == GameState.Game | _gameState == GameState.Pause;
        public bool IsPaused => _gameState == GameState.Pause;
        public Action OnSessionStartEvent, OnSessionEndEvent, OnPauseEvent, OnUnpauseEvent;

        private void Awake()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
            //Time.fixedDeltaTime = 0.0025f;
        }

        public void SubscribeToSessionEvents(ISessionObject iso)
        {
            OnSessionStartEvent += iso.OnSessionStart;
            OnSessionEndEvent += iso.OnSessionEnd;
            OnPauseEvent += iso.OnPause;
            OnUnpauseEvent += iso.OnUnpause;
            if (SessionStarted) iso.OnSessionStart();
            if (IsPaused) iso.OnPause();
        }
        public void UnsubscribeFromSessionEvents(ISessionObject iso)
        {
            OnSessionStartEvent -= iso.OnSessionStart;
            OnSessionEndEvent -= iso.OnSessionEnd;
            OnPauseEvent -= iso.OnPause;
            OnUnpauseEvent -= iso.OnUnpause;
        }

        private void Start()
        {
            ChangeGameState(GameState.Game);
        }

        private void ChangeGameState(GameState gameState)
        {
            if (_gameState == GameState.Game)
            {
                switch (gameState)
                {
                    case GameState.Pause: OnPauseEvent?.Invoke(); break;
                    case GameState.GameFinished: OnSessionEndEvent?.Invoke(); break;
                }
            }
            else
            {
                if (gameState == GameState.Game)
                {
                    switch (_gameState)
                    {
                        case GameState.Pause: OnUnpauseEvent?.Invoke(); break;
                        case GameState.AwaitForStart: OnSessionStartEvent?.Invoke(); break;
                    }
                }
            }
            _gameState = gameState;
        }
    }
}
