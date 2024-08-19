using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private Trajectory _trajectory;
    private Audio _audioManager;
    Animator _animator;
    private Animator enemyAnimator;
    public GameObject _gameObject;
    public bool IndirectAttack;
    private TrailRenderer _trailRenderer;
    private Rigidbody _rb;
    private HealthBar _healthBar;

    private void Start()
    {
        _trajectory = GetComponentInChildren<Trajectory>();
        _animator = GetComponent<Animator>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        enemyAnimator = _gameObject.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        _healthBar = FindObjectOfType<HealthBar>();
    }

    private void Update()
    {
        if (_trajectory.characterAim && Input.GetMouseButtonUp(0))
        {
            IndirectAttack = true;
            AttackAndAnimate();
        }
    }

    private void AttackAndAnimate()
    {
            _animator.SetTrigger("attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _audioManager.PlaySFX(_audioManager.BulletSound);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("MovingPlatform"))
        {
            _trailRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            _rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                _trailRenderer.enabled = true;
            
            }
    }

    public void EnemyAnimation()
    {
        if (enemyAnimator != null)
        {
            _healthBar.SetMaxHealth(100f); // health set to max 
            _audioManager.PlaySFX(_audioManager.EnemyDeath);
            enemyAnimator.SetBool("Death",true);
            Destroy(_gameObject,0.75f);
            IndirectAttack = false;
        }
    }

    public void SwordSound()
    {
        _audioManager.PlaySFX(_audioManager.SwordAttack);
    }
    
    
}
