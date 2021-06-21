// GENERATED AUTOMATICALLY FROM 'Assets/Custom/Scripts/PlayerScripts/New/PC_PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PC_PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PC_PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PC_PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""c8874b2b-3e34-4e44-9941-192b4cee5874"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1a4b7286-076e-4cde-a967-3b4f81ee42ff"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4fe3de86-eaf7-4b74-9487-71f55777a7f9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""6f0ab724-b468-468d-b674-deef32136540"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""77df1f94-74fb-4310-9b59-ce4f519a4c06"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2c5c6fb7-567b-46d7-b43f-791751ee2817"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3bde761b-e234-4a27-8072-80d4a5d14e83"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""585e7aa4-fdd7-4c35-aac1-f300832440f0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5acc211b-3d5b-45d2-a6bc-b452df765616"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b530a9c-2be9-42f9-b46f-fbe523956663"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerActions"",
            ""id"": ""49fcf49c-5191-402f-9624-0831c6d2085c"",
            ""actions"": [
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""713a288b-75f1-4ba7-8120-405366be5141"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MeleeAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b8bbc4cb-1d2f-4929-8402-a42f5210c0b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1bb0cab3-a42c-44ce-9df7-677e852d587d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Casting"",
                    ""type"": ""Button"",
                    ""id"": ""b7ceea76-975b-4156-9e04-7e94f4fc3649"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AirDash"",
                    ""type"": ""Button"",
                    ""id"": ""e37b39cc-7da0-4f5b-b6d0-c391c39005f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CombatMode"",
                    ""type"": ""Button"",
                    ""id"": ""e71fb393-d956-48cf-97f0-61cb093993ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""5d345d5f-6aff-4b5f-acf8-6a3bfef25f60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""56f586c9-4c54-402e-bd31-55146d509cdf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchSpell"",
                    ""type"": ""Button"",
                    ""id"": ""dd1f7a6f-bc4c-4956-8b2a-48c465a516cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""91f1f520-e604-4ac0-a721-5f66fc9b86fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HideUI"",
                    ""type"": ""Button"",
                    ""id"": ""17c1e4fc-df55-4add-916a-291ea338f632"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TP1"",
                    ""type"": ""Button"",
                    ""id"": ""7c233662-6318-4960-bae3-9df3256c63bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TP2"",
                    ""type"": ""Button"",
                    ""id"": ""7bf04573-a1fc-4f90-a4b7-182691e867ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lvl1"",
                    ""type"": ""Button"",
                    ""id"": ""98c1d94f-e282-4d4d-8ee5-1f9dc2e22251"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lvl2"",
                    ""type"": ""Button"",
                    ""id"": ""20c37c49-3410-42e3-a0bc-823084fb58dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lvl3"",
                    ""type"": ""Button"",
                    ""id"": ""5ee9e854-a135-4b19-86eb-b7cf4a0ffa80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lvl4"",
                    ""type"": ""Button"",
                    ""id"": ""0a67db13-ee16-43d0-960f-3c88b7cab988"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TP3"",
                    ""type"": ""Button"",
                    ""id"": ""2c6891e9-aa45-4acc-9e42-fdcd52d9d9ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""61db7c8a-63e7-4492-9a0e-c9945bfa0eb2"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec094c85-76a0-49f4-8fd8-bb793c6ba01e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ea7d46f-2bfd-41bc-899c-c8f3691b1457"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6571ec50-9928-431f-b47b-75103b3761e9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Casting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""faebfbe4-a29b-412c-bae7-e8c4c3947cf1"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AirDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd38b852-5038-47d7-a938-e7d40a85c989"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CombatMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92f6e769-968d-44dc-8aa2-adccd9f59c24"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4b1d380-c934-4f28-989a-309c7a7fae71"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a66a190-7d28-4f8b-a442-24fe9dd8890f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a8a2ad0-4e7e-40b5-adb2-d67b05d54124"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf428c38-f436-4b47-ab7a-ae6e866e853d"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HideUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2361343-f561-44af-9020-e2ee54a4c110"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37ac092a-912d-45d8-ab86-9f97ad06e8e9"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39253cb7-3e72-4009-aed7-75223a404aa8"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lvl1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ffa928c-61e4-44e6-93ac-71a3ba967b81"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lvl2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39e674b7-3283-40ea-9a4b-ed475f3d9d81"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lvl3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f4f6d78-95ef-4116-86a4-70c038a2e72c"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lvl4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d3dca60-7c6e-47d2-9882-1146a5f8c81b"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TP3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Movement
        m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Roll = m_PlayerActions.FindAction("Roll", throwIfNotFound: true);
        m_PlayerActions_MeleeAttack = m_PlayerActions.FindAction("MeleeAttack", throwIfNotFound: true);
        m_PlayerActions_Jump = m_PlayerActions.FindAction("Jump", throwIfNotFound: true);
        m_PlayerActions_Casting = m_PlayerActions.FindAction("Casting", throwIfNotFound: true);
        m_PlayerActions_AirDash = m_PlayerActions.FindAction("AirDash", throwIfNotFound: true);
        m_PlayerActions_CombatMode = m_PlayerActions.FindAction("CombatMode", throwIfNotFound: true);
        m_PlayerActions_Restart = m_PlayerActions.FindAction("Restart", throwIfNotFound: true);
        m_PlayerActions_Interact = m_PlayerActions.FindAction("Interact", throwIfNotFound: true);
        m_PlayerActions_SwitchSpell = m_PlayerActions.FindAction("SwitchSpell", throwIfNotFound: true);
        m_PlayerActions_Pause = m_PlayerActions.FindAction("Pause", throwIfNotFound: true);
        m_PlayerActions_HideUI = m_PlayerActions.FindAction("HideUI", throwIfNotFound: true);
        m_PlayerActions_TP1 = m_PlayerActions.FindAction("TP1", throwIfNotFound: true);
        m_PlayerActions_TP2 = m_PlayerActions.FindAction("TP2", throwIfNotFound: true);
        m_PlayerActions_Lvl1 = m_PlayerActions.FindAction("Lvl1", throwIfNotFound: true);
        m_PlayerActions_Lvl2 = m_PlayerActions.FindAction("Lvl2", throwIfNotFound: true);
        m_PlayerActions_Lvl3 = m_PlayerActions.FindAction("Lvl3", throwIfNotFound: true);
        m_PlayerActions_Lvl4 = m_PlayerActions.FindAction("Lvl4", throwIfNotFound: true);
        m_PlayerActions_TP3 = m_PlayerActions.FindAction("TP3", throwIfNotFound: true);
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

    // Player Movement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    public struct PlayerMovementActions
    {
        private @PC_PlayerControls m_Wrapper;
        public PlayerMovementActions(@PC_PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Roll;
    private readonly InputAction m_PlayerActions_MeleeAttack;
    private readonly InputAction m_PlayerActions_Jump;
    private readonly InputAction m_PlayerActions_Casting;
    private readonly InputAction m_PlayerActions_AirDash;
    private readonly InputAction m_PlayerActions_CombatMode;
    private readonly InputAction m_PlayerActions_Restart;
    private readonly InputAction m_PlayerActions_Interact;
    private readonly InputAction m_PlayerActions_SwitchSpell;
    private readonly InputAction m_PlayerActions_Pause;
    private readonly InputAction m_PlayerActions_HideUI;
    private readonly InputAction m_PlayerActions_TP1;
    private readonly InputAction m_PlayerActions_TP2;
    private readonly InputAction m_PlayerActions_Lvl1;
    private readonly InputAction m_PlayerActions_Lvl2;
    private readonly InputAction m_PlayerActions_Lvl3;
    private readonly InputAction m_PlayerActions_Lvl4;
    private readonly InputAction m_PlayerActions_TP3;
    public struct PlayerActionsActions
    {
        private @PC_PlayerControls m_Wrapper;
        public PlayerActionsActions(@PC_PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Roll => m_Wrapper.m_PlayerActions_Roll;
        public InputAction @MeleeAttack => m_Wrapper.m_PlayerActions_MeleeAttack;
        public InputAction @Jump => m_Wrapper.m_PlayerActions_Jump;
        public InputAction @Casting => m_Wrapper.m_PlayerActions_Casting;
        public InputAction @AirDash => m_Wrapper.m_PlayerActions_AirDash;
        public InputAction @CombatMode => m_Wrapper.m_PlayerActions_CombatMode;
        public InputAction @Restart => m_Wrapper.m_PlayerActions_Restart;
        public InputAction @Interact => m_Wrapper.m_PlayerActions_Interact;
        public InputAction @SwitchSpell => m_Wrapper.m_PlayerActions_SwitchSpell;
        public InputAction @Pause => m_Wrapper.m_PlayerActions_Pause;
        public InputAction @HideUI => m_Wrapper.m_PlayerActions_HideUI;
        public InputAction @TP1 => m_Wrapper.m_PlayerActions_TP1;
        public InputAction @TP2 => m_Wrapper.m_PlayerActions_TP2;
        public InputAction @Lvl1 => m_Wrapper.m_PlayerActions_Lvl1;
        public InputAction @Lvl2 => m_Wrapper.m_PlayerActions_Lvl2;
        public InputAction @Lvl3 => m_Wrapper.m_PlayerActions_Lvl3;
        public InputAction @Lvl4 => m_Wrapper.m_PlayerActions_Lvl4;
        public InputAction @TP3 => m_Wrapper.m_PlayerActions_TP3;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Roll.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @MeleeAttack.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMeleeAttack;
                @Jump.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Casting.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCasting;
                @Casting.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCasting;
                @Casting.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCasting;
                @AirDash.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAirDash;
                @AirDash.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAirDash;
                @AirDash.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAirDash;
                @CombatMode.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCombatMode;
                @CombatMode.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCombatMode;
                @CombatMode.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCombatMode;
                @Restart.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRestart;
                @Interact.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @SwitchSpell.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSwitchSpell;
                @SwitchSpell.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSwitchSpell;
                @SwitchSpell.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSwitchSpell;
                @Pause.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPause;
                @HideUI.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnHideUI;
                @HideUI.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnHideUI;
                @HideUI.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnHideUI;
                @TP1.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTP1;
                @TP1.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTP1;
                @TP1.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTP1;
                @TP2.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTP2;
                @TP2.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTP2;
                @TP2.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTP2;
                @Lvl1.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl1;
                @Lvl1.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl1;
                @Lvl1.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl1;
                @Lvl2.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl2;
                @Lvl2.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl2;
                @Lvl2.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl2;
                @Lvl3.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl3;
                @Lvl3.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl3;
                @Lvl3.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl3;
                @Lvl4.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl4;
                @Lvl4.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl4;
                @Lvl4.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLvl4;
                @TP3.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTP3;
                @TP3.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTP3;
                @TP3.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnTP3;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @MeleeAttack.started += instance.OnMeleeAttack;
                @MeleeAttack.performed += instance.OnMeleeAttack;
                @MeleeAttack.canceled += instance.OnMeleeAttack;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Casting.started += instance.OnCasting;
                @Casting.performed += instance.OnCasting;
                @Casting.canceled += instance.OnCasting;
                @AirDash.started += instance.OnAirDash;
                @AirDash.performed += instance.OnAirDash;
                @AirDash.canceled += instance.OnAirDash;
                @CombatMode.started += instance.OnCombatMode;
                @CombatMode.performed += instance.OnCombatMode;
                @CombatMode.canceled += instance.OnCombatMode;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @SwitchSpell.started += instance.OnSwitchSpell;
                @SwitchSpell.performed += instance.OnSwitchSpell;
                @SwitchSpell.canceled += instance.OnSwitchSpell;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @HideUI.started += instance.OnHideUI;
                @HideUI.performed += instance.OnHideUI;
                @HideUI.canceled += instance.OnHideUI;
                @TP1.started += instance.OnTP1;
                @TP1.performed += instance.OnTP1;
                @TP1.canceled += instance.OnTP1;
                @TP2.started += instance.OnTP2;
                @TP2.performed += instance.OnTP2;
                @TP2.canceled += instance.OnTP2;
                @Lvl1.started += instance.OnLvl1;
                @Lvl1.performed += instance.OnLvl1;
                @Lvl1.canceled += instance.OnLvl1;
                @Lvl2.started += instance.OnLvl2;
                @Lvl2.performed += instance.OnLvl2;
                @Lvl2.canceled += instance.OnLvl2;
                @Lvl3.started += instance.OnLvl3;
                @Lvl3.performed += instance.OnLvl3;
                @Lvl3.canceled += instance.OnLvl3;
                @Lvl4.started += instance.OnLvl4;
                @Lvl4.performed += instance.OnLvl4;
                @Lvl4.canceled += instance.OnLvl4;
                @TP3.started += instance.OnTP3;
                @TP3.performed += instance.OnTP3;
                @TP3.canceled += instance.OnTP3;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnRoll(InputAction.CallbackContext context);
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCasting(InputAction.CallbackContext context);
        void OnAirDash(InputAction.CallbackContext context);
        void OnCombatMode(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSwitchSpell(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnHideUI(InputAction.CallbackContext context);
        void OnTP1(InputAction.CallbackContext context);
        void OnTP2(InputAction.CallbackContext context);
        void OnLvl1(InputAction.CallbackContext context);
        void OnLvl2(InputAction.CallbackContext context);
        void OnLvl3(InputAction.CallbackContext context);
        void OnLvl4(InputAction.CallbackContext context);
        void OnTP3(InputAction.CallbackContext context);
    }
}
