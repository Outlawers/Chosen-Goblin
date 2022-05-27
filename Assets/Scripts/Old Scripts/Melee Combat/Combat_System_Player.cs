using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


//System that orchastrates players combat, and allocates appriopriate classes to action took by player
public class Combat_System_Player : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _inputMap;

    private InputAction _castSpell;
    private InputAction _swordAttack;

    private Combat_Magic_Player_Component _magicComponent;
    private Combat_PLayer_Sword_Component _swordComponent;

    //camera is used to calculate path for spells
    private Camera _camera;

    void Awake()
    {
        _castSpell = _inputMap.actions["MagicCast"];
        _swordAttack = _inputMap.actions["SwordHit"];
        _magicComponent = gameObject.GetComponent<Combat_Magic_Player_Component>();
        _swordComponent = gameObject.GetComponent<Combat_PLayer_Sword_Component>();
        _camera = Camera.main;   
    }

    private void OnEnable()
    {
        _castSpell.performed += _ => _magicComponent.CastSpell(_camera);
        _swordAttack.performed += _ => _swordComponent.Attack();
      
        
    }

    private void OnDisable()
    {
        _castSpell.performed -= _ => _magicComponent.CastSpell(_camera);
        _swordAttack.performed -= _ => _swordComponent.Attack();
    }

}
