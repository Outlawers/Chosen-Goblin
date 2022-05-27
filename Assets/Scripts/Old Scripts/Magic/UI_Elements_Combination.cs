using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Elements_Combination : MonoBehaviour
{
    //Assign all the buttons in the MENU
    [SerializeField]
    private Button _fireButton;
    [SerializeField]
    private Button _waterButton;
    [SerializeField]
    private Button _projectile;
    [SerializeField]
    private Button _wall;

    //Create a variable to store desired element
    [SerializeField]
    private string _elementChosen;
    [SerializeField]
    private string _kindChosen;


    private void Start()
    {
        _elementChosen = "Fire";
        _fireButton.onClick.AddListener(ChooseFire);
        _waterButton.onClick.AddListener(ChooseWater);
        _projectile.onClick.AddListener(ChooseProjectile);
        _wall.onClick.AddListener(ChooseWall);
    }

    public string GetChosenElement()
    {
        return _elementChosen;
    }

    public string GetChosenKind()
    {
        return _kindChosen;
    }

    //after chosing the lement from manu
    private void ChooseFire()
    {
        //if we already have one elemnt choosen
        if(_elementChosen == "Water")
        {
            //combine
            _elementChosen = "Fire+Water";
        }
        else
        {
            //Set element to fire
            _elementChosen = "Fire";
        }
    }
    private void ChooseWater()
    {
        if (_elementChosen == "Fire")
        {
            _elementChosen = "Fire+Water";
        }
        else
        {
            _elementChosen = "Water";
        }
    }

    private void ChooseProjectile()
    {
        if(_kindChosen != "Projectile")
        {
            _kindChosen = "Projectile";
        }
        else
        {
            Debug.Log("Projectile was already chosen");
        }
    }

    private void ChooseWall()
    {
        if(_kindChosen != "Wall")
        {
            _kindChosen = "Wall";
        }
        else
        {
            Debug.Log("Wall was already chosen");
        }
    }
}
