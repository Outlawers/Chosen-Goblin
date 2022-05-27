using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class CombatOnButton : MonoBehaviour
{
    [SerializeField]
    private Animator _playerAnim;

    private StarterAssetsInputs _input;

    public void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }
    public void Attack()
    {
        _playerAnim.SetBool("Attack", true);
    }
    

    // Update is called once per frame
    void Update()
    {
        //add attack rate value
        /*if (_input.attack)
        {
            Attack();
        }*/
    }
    
}
