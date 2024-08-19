using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform PosA, PosB;
    public float Speed;
    private Rigidbody rb;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = PosB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PosA.position) < 0.1f) targetPos = PosB.position;

        if (Vector3.Distance(transform.position, PosB.position) < 0.1f) targetPos = PosA.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
            rb = other.GetComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            other.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(PosA.position, PosB.position );
    }
}