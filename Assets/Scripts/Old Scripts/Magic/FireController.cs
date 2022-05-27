using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Brain of the spell that is responsible for spells behaviour
 */
public class FireController : MonoBehaviour
{
    //Game object spawned after the spell connects
    [SerializeField]
    private GameObject _bulletDecal;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timeToDestroy;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private ParticleSystem _trail;

    private Vector3 _target;
    private bool _hit;

    private void OnEnable()
    {
        Debug.Log("I Was enabled!");
       // Destroy(gameObject, _timeToDestroy);
    }

    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        Debug.Log("I Hit something!");
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    public void SetHit(bool hit)
    {
        _hit = hit;
    }

    public void Behaviour()
    {
        //make bullet go in desried direction   
        gameObject.transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        if (!_hit && Vector3.Distance(transform.position, _target) < 0.01)
        {
            Debug.Log("I Hit something!");
            Destroy(this.gameObject);
        }
        //destroy after set amount of time
        Destroy(this.gameObject, _timeToDestroy);
    }

    void Update()
    {
        Behaviour();
    }

   
    
}
