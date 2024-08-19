using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    public Transform PosA, PosB;
    public float Speed;
    private Rigidbody rb;
    Vector3 targetPos;
    public float rotationSpeed;
    private GameObject _characterObject;
    private CharacterHealth _characterHealth;
    private Audio _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _characterObject = GameObject.FindWithTag("Player");
        _characterHealth = _characterObject.GetComponent<CharacterHealth>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        targetPos = PosB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PosA.position) < 0.1f) targetPos = PosB.position;

        if (Vector3.Distance(transform.position, PosB.position) < 0.1f) targetPos = PosA.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _audioManager.PlaySFX(_audioManager.BladeHit);
            _characterHealth.BladeDamage();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(PosA.position, PosB.position);
    }
    



}
