using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * System responsible for casting a chosen spell
 */

public class Combat_Magic_Player_Component : MonoBehaviour
{
    
    [SerializeField]
    //Set point from which spell will be started
    private Transform _castTransform;
    [SerializeField]
    private float spread;
    [SerializeField]
    private float range;
    [SerializeField]
    //camera used to determin direction of the spell
    private Cinemachine.CinemachineVirtualCamera _aimCamera;

    //current spell
    private Spell _cast;
    //Ray used to show me where spell's 
    private LineRenderer laserLine;

    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        //Current spell is aquired every time player wants to cast from a Spell component
        _cast = GetComponent<Spell>();
    }
    public void CastSpell(Camera _camera)
    {
        _cast.CreateAndSetSpell(_aimCamera, _camera, _castTransform);      
    }
}
