using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Component : MonoBehaviour, IPlayer_Animation_Component
{
    [SerializeField]
    private Animator _movementAnimation;

    public void MovementAnimationSystem(Vector2 input)
    {
        if (input.y > 0)
        {
            _movementAnimation.SetFloat("Forward", 1);
        }
        if (input.y < 0)
        {
            _movementAnimation.SetFloat("Forward", -1);
        }
        if (input.x > 0)
        {
            _movementAnimation.SetFloat("Strafe", 1);
        }
        if (input.x < 0)
        {
            _movementAnimation.SetFloat("Strafe", -1);
        }
        if (input.x == 0 && input.y == 0)
        {
            _movementAnimation.SetFloat("Forward", 0);
            _movementAnimation.SetFloat("Strafe", 0);
        }
    }
}
