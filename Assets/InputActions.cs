//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""ActionMap"",
            ""id"": ""2c6b6667-70f3-450b-857d-185d7f6e9d9a"",
            ""actions"": [
                {
                    ""name"": ""PlayerMovement"",
                    ""type"": ""Value"",
                    ""id"": ""1533383c-4bcf-4c5f-bde1-da57eeb5f30a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PlayerShoot"",
                    ""type"": ""Button"",
                    ""id"": ""92dc6076-e959-457b-b094-ad40351f0b08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""8ff0157a-8dbc-45f1-a5bf-1c345ceab12b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""46323e3c-b0c9-4749-8e94-0dc94e3fd122"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1d243f82-8486-4a7e-b827-2776bf051443"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e75ce598-41d5-4a43-8b7e-c2be6cb8a0f3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dc17f566-e271-42fd-b0a3-bff2937f51c6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""bc8876c4-82f4-427f-b788-d7cfc963107a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8700e819-39c0-4f43-9976-7d3b793ab4de"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e494a5eb-4960-4dfd-9b5c-99604db6071f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""109b306a-d8e4-4ef8-9d4a-cbac11630640"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bca7ba0e-9371-43f8-a8de-f40bda201271"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1bde1c78-db89-4a3f-8f28-7e244f365e60"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ActionMap
        m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
        m_ActionMap_PlayerMovement = m_ActionMap.FindAction("PlayerMovement", throwIfNotFound: true);
        m_ActionMap_PlayerShoot = m_ActionMap.FindAction("PlayerShoot", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // ActionMap
    private readonly InputActionMap m_ActionMap;
    private List<IActionMapActions> m_ActionMapActionsCallbackInterfaces = new List<IActionMapActions>();
    private readonly InputAction m_ActionMap_PlayerMovement;
    private readonly InputAction m_ActionMap_PlayerShoot;
    public struct ActionMapActions
    {
        private @InputActions m_Wrapper;
        public ActionMapActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMovement => m_Wrapper.m_ActionMap_PlayerMovement;
        public InputAction @PlayerShoot => m_Wrapper.m_ActionMap_PlayerShoot;
        public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
        public void AddCallbacks(IActionMapActions instance)
        {
            if (instance == null || m_Wrapper.m_ActionMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ActionMapActionsCallbackInterfaces.Add(instance);
            @PlayerMovement.started += instance.OnPlayerMovement;
            @PlayerMovement.performed += instance.OnPlayerMovement;
            @PlayerMovement.canceled += instance.OnPlayerMovement;
            @PlayerShoot.started += instance.OnPlayerShoot;
            @PlayerShoot.performed += instance.OnPlayerShoot;
            @PlayerShoot.canceled += instance.OnPlayerShoot;
        }

        private void UnregisterCallbacks(IActionMapActions instance)
        {
            @PlayerMovement.started -= instance.OnPlayerMovement;
            @PlayerMovement.performed -= instance.OnPlayerMovement;
            @PlayerMovement.canceled -= instance.OnPlayerMovement;
            @PlayerShoot.started -= instance.OnPlayerShoot;
            @PlayerShoot.performed -= instance.OnPlayerShoot;
            @PlayerShoot.canceled -= instance.OnPlayerShoot;
        }

        public void RemoveCallbacks(IActionMapActions instance)
        {
            if (m_Wrapper.m_ActionMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IActionMapActions instance)
        {
            foreach (var item in m_Wrapper.m_ActionMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ActionMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ActionMapActions @ActionMap => new ActionMapActions(this);
    public interface IActionMapActions
    {
        void OnPlayerMovement(InputAction.CallbackContext context);
        void OnPlayerShoot(InputAction.CallbackContext context);
    }
}
