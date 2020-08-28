using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.CompareTag("Player")) {
            _animator.Play("Breaking Barrel");
            BoxCollider2D _box =  GetComponent<BoxCollider2D>();
            _box.enabled = false;
        }
    }
}
