using System.Collections;
using UnityEngine;

public class FieldofView : MonoBehaviour
{
    public float radius;
    [Range(0,180)]
    public float angle;

    public LayerMask targetLayer;
    public LayerMask obstructionsLayer;

    [HideInInspector] public GameObject playerRef;
    private CapsuleCollider _capsuleCollider;
    private float _height;
    private Vector3 _scale;
    private Animator _animator;
   [HideInInspector] public Vector3 objectCenter;
   [HideInInspector] public Vector3 targetCenter;
   [HideInInspector] public bool canSeePlayer;
    
    void Start()
    {
      playerRef = GameObject.FindGameObjectWithTag("Player");
      _capsuleCollider = GetComponent<CapsuleCollider>();
      _height = _capsuleCollider.height / 2 + 1.3f; // 1.3f to move slightly up (eye-level instead of center of mass)
      _scale = transform.localScale;
      objectCenter = transform.position + _scale.x * new Vector3(0, _height, 0);
      _animator = GetComponent<Animator>();
      StartCoroutine(FOVRoutine());
    }

    private void Update()
    {

    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while(true)
        {
            yield return wait;
            FieldofViewCheck();
        }
    }
    private void FieldofViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(objectCenter, radius, targetLayer);
        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            // Adjusting for incorrect center/pivot position, adjusting the vector for slightly above waist
            targetCenter = target.position + (0.35f * new Vector3(0, 3.3f, 0));
            Vector3 directionToTarget = (targetCenter - objectCenter).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(objectCenter, targetCenter);
                if (!Physics.Raycast(objectCenter, directionToTarget, distanceToTarget, obstructionsLayer))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            } else
            {
                canSeePlayer = false;
            }

        } else if (canSeePlayer)
        {
            _animator.SetBool("Shooting", false);
            _animator.SetBool("Alert",false);
            canSeePlayer = false;
        }
        
    }
   
}
