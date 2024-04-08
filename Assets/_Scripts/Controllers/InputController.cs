using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using ZE.ServiceLocator;

namespace ZE.IsohECS {
	public sealed class InputController : SessionObject
	{
        private bool _selectionPressed = false, _frameSelectionEnabled = false;
        private Vector2 _startMousePos, _mousePos;
        private ClickHandler _clickHandler;

        private readonly float MIN_MOUSE_DRAG = Screen.height * 0.05f;
        public Rect SelectionFrameRect => new (
            math.min(_startMousePos.x, _mousePos.x),
            math.min(_startMousePos.y, _mousePos.y),
            math.abs(_startMousePos.x - _mousePos.x),
            math.abs(_startMousePos.y - _mousePos.y));

        public Action<Vector2> OnCameraMoveEvent { get; set; }
        public Action<float> OnCameraZoomEvent { get; set; }

        public Action OnSelectionFrameStartEvent, OnSelectionFrameStopEvent;


        protected override void OnStart()
        {
            _clickHandler = ServiceLocatorObject.Get<ClickHandler>();
        }
        private void Update()
        {
            if (GameSessionActive)
            {
                _mousePos = Input.mousePosition;
                if (Input.GetMouseButton(0))
                {
                    // mouse pressed
                    if (!_selectionPressed)
                    {
                        _startMousePos = _mousePos;
                        _selectionPressed = true;
                    }
                    else
                    {
                        float sqrdelta = Vector2.SqrMagnitude(_mousePos - _startMousePos);
                        bool enableFrame = sqrdelta > MIN_MOUSE_DRAG * MIN_MOUSE_DRAG;
                        if (enableFrame != _frameSelectionEnabled)
                        {
                            if (enableFrame)
                            {
                                _frameSelectionEnabled = true;
                                OnSelectionFrameStartEvent?.Invoke();
                            }
                            else
                            {
                                _frameSelectionEnabled = false;
                                OnSelectionFrameStopEvent?.Invoke();
                            }
                        }
                    }
                }
                else
                {
                    // mouse not pressed
                    if (_selectionPressed)
                    {
                        if (_frameSelectionEnabled)
                        {
                            // multiple targets
                            _clickHandler.SelectFrame(SelectionFrameRect);
                        }
                        else
                        {
                            // single target
                            _clickHandler.SelectClick(Input.mousePosition);
                        }
                        StopSelectionFrame();
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            _clickHandler.ContextClick(Input.mousePosition);
                        }
                    }
                }

                float x = Input.GetAxis("Horizontal"), z = Input.GetAxis("Vertical");
                if (x != 0f || z != 0f)
                {
                    OnCameraMoveEvent?.Invoke(new Vector2(x, z));
                }

                float zoom = Input.GetAxis("Mouse ScrollWheel");
                if (zoom != 0f)
                {
                    OnCameraZoomEvent?.Invoke(zoom);
                }
            }
        }

        public override void OnPause()
        {
            base.OnPause();
            StopSelectionFrame();
        }
        public override void OnSessionEnd()
        {
            base.OnSessionEnd();
            StopSelectionFrame();
        }
        private void StopSelectionFrame()
        {
            _selectionPressed = false;
            if (_frameSelectionEnabled)
            {
                _frameSelectionEnabled = false;
                OnSelectionFrameStopEvent?.Invoke();
            }
        }
    }
}
