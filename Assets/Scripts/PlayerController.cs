using StarterAssets;
using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using System.Collections.Generic;
#endif

namespace Scripts
{
    /// <summary>
    /// Name: 
    /// Date: 
    /// Version: 
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Under this section in-Unity value changing are declared and set to a default value
        /// </summary>
        #region Unity Edits

        [Header("Attack")]
        [Tooltip("Time required to pass before being able to attack again. Set to 0f to instantly attack again")]
        public float AttackTimeout = 2.28f;

        #endregion

        #region Fields

        // timeout deltatime
        private float _attackTimeoutDelta;

        // animation IDs
        private int _animIDAttack;

        private bool pStopAttacking = false;
        private int _animIDAttack2;
        private int _animIDMagic;

        private StarterAssetsInputs _input;
        private Animator _animator;

        private bool _hasAnimator;

        private InputMemory _inputMemory;


        

        #endregion

        #region Properties



        #endregion

        #region Methods

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _input = GetComponent<StarterAssetsInputs>();

            AssignAnimationIDs();

            // reset our timeouts on start
            _attackTimeoutDelta = AttackTimeout;

            _inputMemory = GetComponent<InputMemory>();
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);
            
            Attack();    
            
            
        }

        private void AssignAnimationIDs()
        {
            _animIDAttack = Animator.StringToHash("Attack");
            _animIDAttack2 = Animator.StringToHash("Attack2");
            _animIDMagic = Animator.StringToHash("Magic1");
        }

        private void Attack()
        {
			
			// reset the fall timeout timer
			_attackTimeoutDelta = AttackTimeout;

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetBool(_animIDAttack, false);

			}
            
            //Debug.Log(_attackTimeoutDelta);
            // Jump
           if (_input.attack)// && _attackTimeoutDelta <= 0.0f)
			{               
                // update animator if using character
                if (_hasAnimator && _animator.GetBool(_animIDAttack) != true)
				{                   
					_animator.SetBool(_animIDAttack, true);

                    //Debug.Log(_inputMemory.Count);
                    //Debug.Log("test");
				}
			}

			// jump timeout
			if (_attackTimeoutDelta >= 0.0f)
			{
				_attackTimeoutDelta -= Time.deltaTime;
			}
			
		}


        public void DefinitiveStop()
        {
            _inputMemory.ClearInputMemory();
            _input.attack = false;
            _animator.SetBool(_animIDAttack, false);
            _animator.SetBool(_animIDAttack2, false);
            _animator.SetBool(_animIDMagic, false);
        }
        public void StopAttack1()
        {
            if(_inputMemory.CheckInputMemoryCount() == 1)
            {
                DefinitiveStop();
            }
            // Left input - activate melee weapon attack
            if (_inputMemory.CheckInputMemoryLastInput() == 'L' && _inputMemory.CheckInputMemoryCount() > 1)
            {
                _animator.SetBool(_animIDAttack, false);
                _animator.SetBool(_animIDAttack2, true);                
                _animator.SetBool(_animIDMagic, false);
            }
            // Right input - activate magic attack
            if(_inputMemory.CheckInputMemoryLastInput() == 'R' && _inputMemory.CheckInputMemoryCount() > 1)
            {                
                _animator.SetBool(_animIDAttack, false);
                _animator.SetBool(_animIDAttack2, false);
                _animator.SetBool(_animIDMagic, true);
            }
        
        }

        private bool StopAttacking()
        {
            _input.attack = false;

            return _input.attack;
        }

        #endregion
    }
}
