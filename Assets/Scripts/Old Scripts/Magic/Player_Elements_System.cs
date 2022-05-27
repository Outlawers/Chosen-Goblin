using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Elements_System : MonoBehaviour
{
    [SerializeField]
    private Spell _bookOfSpells;
    [SerializeField]
    private Canvas _elementalCanvas;
    [SerializeField]
    private UI_Elements_Combination _combinationResult;
    [SerializeField]
    private PlayerInput _inputMap;

    private InputAction _changeMagic;


    void Awake()
    {
        _changeMagic = _inputMap.actions["ChangeElements"];
        _bookOfSpells = gameObject.GetComponent<Spell>();
        _elementalCanvas.enabled = false;

    }
    private void OnEnable()
    {
        _changeMagic.performed += _ => OpenMenu();

        _changeMagic.canceled += _ => CloseMenu();
    }

    private void OnDisable()
    {
        _changeMagic.performed -= _ => OpenMenu();

        _changeMagic.canceled -= _ => CloseMenu();
    }

    private void OpenMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;        
        _elementalCanvas.enabled = true;
    }

    private void CloseMenu()
    {      
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _elementalCanvas.enabled = false;
        _bookOfSpells.ChangeElement(_combinationResult.GetChosenKind(), _combinationResult.GetChosenElement());
    }
}
