using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerDashComponent : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float _dashTime;
    [SerializeField]
    private float _dashSpeed;
    [SerializeField]
    private float _dashCooldown;
    [SerializeField]
    private float nextDashTime;

    #endregion

    #region Dash Logic
    public IEnumerator Dash(Vector3 _direction, CharacterController _character)
    {
        float startTime = Time.time;
        if (startTime > nextDashTime)
        {
            while (Time.time < startTime + _dashTime)
            {
                _character.Move(_direction * _dashSpeed * Time.deltaTime);
                yield return null;
            }
            nextDashTime = startTime + _dashCooldown;
        }

    }
    #endregion
}
