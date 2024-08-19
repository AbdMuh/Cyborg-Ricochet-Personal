using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDistance = 8f;
    [SerializeField] private LayerMask ignoreMask;
    [SerializeField] private UnityEvent OnHitTarget;

    private RaycastHit rayHit;
    private GameObject _characterObject;
    private CharacterHealth _characterHealth;
    private Ray ray;
    private Audio _audioManager;

    [SerializeField] private float laserTime;

    private bool state;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        state = true;
        lineRenderer.positionCount = 2;

        InvokeRepeating("ToggleLaser", laserTime, laserTime);
        _characterObject = GameObject.FindWithTag("Player");
           _characterHealth = _characterObject.GetComponent<CharacterHealth>();
           _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();

    }

    void ToggleLaser()
    {
        if (state)
        {
            state = false;
            lineRenderer.enabled = false;
        }
        else
        {
            state = true;
            lineRenderer.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ray = new(transform.position, transform.forward);

        if (state)
        {
            if (Physics.Raycast(ray, out rayHit, laserDistance, ~ignoreMask))
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, rayHit.point);
                if (rayHit.collider.gameObject.CompareTag("Player"))
                {
                    _audioManager.PlaySFX(_audioManager.LaserHit);
                    _characterHealth.LaserDamage();
                }
            }
            else
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + transform.forward * laserDistance);
            }
        }
    }

        private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, ray.direction * laserDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(rayHit.point, 0.23f);
    }
}
