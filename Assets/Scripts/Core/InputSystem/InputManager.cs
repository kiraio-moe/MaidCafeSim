using UnityEngine;
using UnityEngine.InputSystem;

namespace MaidCafe.Core.InputSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        InputSettings inputSettings;

        void Awake()
        {
            inputSettings = new InputSettings();
        }
        
        void OnEnable()
        {
            inputSettings.Enable();
        }
        
        void OnDisable()
        {
            inputSettings.Disable();
        }
    }
}
