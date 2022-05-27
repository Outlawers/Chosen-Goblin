using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine;

/*
 * Short system that allows to switch between two cameras, camera that allows to use magic, and camera that allows to use sword
 * Class is based on turning on and off the aiming camera treating third person camera as a base
 */

public class SwitchVcam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _inputMap;
    [SerializeField]
    private int _cameraPriorityChange;
    [SerializeField]
    private Canvas _thirdPersonCanvas;
    [SerializeField]
    private Canvas _aimCanvas;

    private bool _amIAiming;
    private CinemachineVirtualCamera _virtualCamera;
    private InputAction _aimAction;
    
    private void Awake()
    {
        _aimCanvas.enabled = false;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _aimAction = _inputMap.actions["Magic"];
    }

    //switch camera to aiming
    private void StartAim()
    {
        //set flag to true
        _amIAiming = true;
        //change camera priority (base is 10)
        _virtualCamera.Priority += _cameraPriorityChange;
        //Switch UI with cross hair to make a clear differance
        _aimCanvas.enabled = true;
        //turn off previous crosshair
        _thirdPersonCanvas.enabled = false;
    }

    private void CancelAim()
    {
        _amIAiming = false;
        _virtualCamera.Priority -= _cameraPriorityChange;
        _aimCanvas.enabled = false;
        _thirdPersonCanvas.enabled = true;
    }

    public  bool GetAmIAiming()
    {
        return _amIAiming;
    }

    private void OnEnable()
    {
        _aimAction.performed += _ => StartAim();

        _aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        _aimAction.performed -= _ => StartAim();

        _aimAction.canceled -= _ => CancelAim();
    }
}
