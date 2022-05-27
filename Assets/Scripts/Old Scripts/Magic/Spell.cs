using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Method that changes the elemnt of your spell
public class Spell : MonoBehaviour
{
    //Create element variables
    [SerializeField]
    private GameObject _fire;
    [SerializeField]
    private GameObject _water;
    [SerializeField]
    private GameObject _fire_water;
    [SerializeField]
    private GameObject _fireWall;
    [SerializeField]
    private GameObject _waterWall;

    [SerializeField]
    //Variable to hold current element
    private GameObject _currentElement;
    private string _currentKind;
    [SerializeField]
    //Variable used to affect the sword
    /*Create interface*/private Sword_ChangeColor _swordElement;

    private FireController _spellMind;

    /*************************************/
    [SerializeField]
    private float range;
    [SerializeField]
    //Set _spell parent to clean the Hierarhy screen
    private Transform _spellParent;

    private void Start()
    {
        _currentElement = _fire;

        //_spellMind = new FireController();   
    }

    public void SetFireController(GameObject _spell, Vector3 _target, bool _hit)
    {
        _spellMind = _spell.GetComponent<FireController>();
        _spellMind.SetTarget(_target);
        _spellMind.SetHit(_hit);
    }

   //Method responsible for changing the elements of current spell by external sources
    public void ChangeElement(string kind, string Element)
    {
        _currentKind = kind;
       if(Element == "Fire" && kind == "Projectile")
        {
            ChangeToFire();
            Element = "Current";
        }
        if (Element == "Fire" && kind == "Wall")
        {
            ChangeToFireWall();
            Element = "Current";
        }
        if (Element == "Water" && kind == "Projectile")
        {
            ChangeToWater();
            Element = "Current";
        }
        if (Element == "Water" && kind == "Wall")
        {
            ChangeToWaterWall();
            Element = "Current";
        }
        if (Element == "Fire+Water" && kind == "Projectile")
        {
            CombineFireWater();
            Element = "Current";
        }
        if (Element == "Fire+Water" && kind == "Wall")
        {
            CombineFireWaterFireWall();
            Element = "Current";
        }
    }

    private void CombineFireWaterFireWall()
    {
        _currentElement = _waterWall;
        Debug.Log("Water + Fire Wall not implemented");
    }

    private void ChangeToWaterWall()
    {
        _currentElement = _waterWall;
        _swordElement.Water();
    }

    private void ChangeToFireWall()
    {
        _currentElement = _fireWall;
        _swordElement.Fire();
    }

    //Methods to cahnge the properties of the spell
    private void ChangeToWater()
    {
        _currentElement = _water;
        _swordElement.Water();
    }

    private void ChangeToFire()
    {
        _currentElement = _fire;
        _swordElement.Fire();
    }

    private void CombineFireWater()
    {
        _currentElement = _fire_water;
        _swordElement.FireWater();
    }

    public void CreateAndSetSpell(Cinemachine.CinemachineVirtualCamera _Aimcamera, Camera _camera, Transform _spellOrigin)
    {
        //if player is aiming
        if (_Aimcamera.m_Priority > 10 && _currentKind == "Projectile")
        {
            //create point from which spell will go
            Vector3 _reyOrigin = _camera.ViewportToWorldPoint(_spellOrigin.position);
            //Create cariable to store the object that has been hit
            RaycastHit hit;
            LineRenderer laserLine = GetComponent<LineRenderer>();
            //Create the trajectory
            laserLine.SetPosition(0, _spellOrigin.position);

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit))
            {
                laserLine.SetPosition(1, hit.point);
                GameObject _spell = GameObject.Instantiate(_currentElement, _spellOrigin.position, Quaternion.identity, _spellParent);
                SetFireController(_spell, hit.point, true);
            }
            else
            {
                laserLine.SetPosition(1, _reyOrigin + (_camera.transform.forward * range));
                GameObject _spell = GameObject.Instantiate(_currentElement, _spellOrigin.position, Quaternion.identity, _spellParent);
                SetFireController(_spell, _camera.transform.position + (_camera.transform.forward * range), false);
            }
        }
        if(_Aimcamera.m_Priority > 10 && _currentKind == "Wall")
        {
            //create point from which spell will go
            Vector3 _reyOrigin = _camera.ViewportToWorldPoint(_spellOrigin.position);
            //Create cariable to store the object that has been hit
            RaycastHit hit;

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Floor"))
                {
                    Vector3 YOffset = new Vector3(0, 1, 0);
                    GameObject _spell = GameObject.Instantiate(_currentElement, hit.point + YOffset, Quaternion.identity, _spellParent);
                }           
            }
            else
            {
                Debug.Log("You are not aiming in to the Floor");
            }
        }
        else
        {
            Debug.Log("Wrong Camera");
        }
    }
}
