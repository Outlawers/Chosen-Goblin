using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Method responsible for changing the color of the sword
 */
public class Sword_ChangeColor : MonoBehaviour
{
    [SerializeField]
    private Texture _fireTexture;
    [SerializeField]
    private Texture _waterTexture;
    [SerializeField]
    private Texture _fire_waterTexture;
    [SerializeField]
    private Renderer _renderer;
   

    public void Water()
    {

        _renderer.material.mainTexture = _waterTexture;
        
    }

    public void Fire()
    {

        _renderer.material.mainTexture = _fireTexture;

    }
    public void FireWater()
    {

        _renderer.material.mainTexture = _fire_waterTexture;

    }
}
