﻿using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class GetPlayerInputSystem : SystemBase
{
    private InputActions _inputActions;
    private Entity _playerEntity;
        
    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMovementInput>();

        _inputActions = new InputActions();
    }

    protected override void OnStartRunning()
    {
        _inputActions.Enable();
        _inputActions.ActionMap.PlayerShoot.performed += OnPlayerShoot;
            
        _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    protected override void OnUpdate()
    {
        var currentMoveInput = _inputActions.ActionMap.PlayerMovement.ReadValue<Vector2>();

        SystemAPI.SetSingleton(new PlayerMovementInput { Value = currentMoveInput });
    }

    protected override void OnStopRunning()
    {
        _inputActions.ActionMap.PlayerShoot.performed -= OnPlayerShoot;
        _inputActions.Disable();
            
        _playerEntity = Entity.Null;
    }

    private void OnPlayerShoot(InputAction.CallbackContext obj)
    {
        if (!SystemAPI.Exists(_playerEntity)) return;
            
        SystemAPI.SetComponentEnabled<PlayerProjectileTag>(_playerEntity, true);
    }
}