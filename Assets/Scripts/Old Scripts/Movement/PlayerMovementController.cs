using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;



/*
 *Class that is responsible for character movement. 
 *System is based on Input Map, and Move input action.
 */

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMovementController : MonoBehaviour
{
    //Camrea transform is required to move hcaracter in relation to the direction player looks at. 
    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float _rotationSpeed;

    private CharacterController _controller;
    private Vector3 _velocity;
    private PlayerInput _inputMap;
    private InputAction _moveAction;
    private IPlayer_Animation_Component _anim;

    private PlayerDashComponent _dash;
    private InputAction _dashAction;

    void Start()
    {
        //This system is also used to lock the cursor out.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Assign all the variables
        _controller = GetComponent<CharacterController>();
        _inputMap = GetComponent<PlayerInput>();
        _dash = GetComponent<PlayerDashComponent>();
        _anim = GetComponent<Player_Animation_Component>();
        _cameraTransform = Camera.main.transform;
        _moveAction = _inputMap.actions["Move"];
        _dashAction = _inputMap.actions["Dash"];
    }

    //Movement system method
    private Vector3 MovementSystem()
    { 
        //create two Vectors, Vector2 input to read input and Vector3 move to change the position of character in relation to input values.
        Vector2 input = _moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        //amend Vector3 move to synchronize it with camera
        move = move.x * _cameraTransform.right.normalized + move.z * _cameraTransform.forward.normalized;
        move.y = 0f;
        //move character
        _controller.Move(move * playerSpeed * Time.deltaTime);
        //play movement animation blend tree using given input
        _anim.MovementAnimationSystem(input);
        //Dash system (work in progress)
        if (_dashAction.triggered)
        {
            if (move != Vector3.zero)
            {
                StartCoroutine(_dash.Dash(move, _controller));
            }
            else
            {
                Vector3 moveForward = new Vector3(0, 0, 1);
                StartCoroutine(_dash.Dash(moveForward, _controller));
            }
        } 
        return move;      
    }

    //Mouse rotation method used to rotate character in relation to mouse movement
    private void MouseRotation()
    {
        Quaternion _rotation = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, _rotationSpeed * Time.deltaTime);
    }


    void Update()
    {
        MouseRotation();
        MovementSystem();
    }
}
