using UnityEngine;


public class Trajectory : MonoBehaviour
{
    [SerializeField] LayerMask nonPlayer;
    private LineRenderer _lr;
    public Vector3 tempVec;
    bool _flag;  
    public int bounceCount;
    public bool characterAim;

    Color _c1 = new Color(0.98f, 0.631f,0, 1);
    
    Color _c2 = new Color(0.0f, 0.757f, 0.980f);

    Color _c3 = new Color(0.980f, 0.318f, 0.0f);


    void Start()
    {
        _lr = GetComponent<LineRenderer>();
        _lr.numCapVertices = 20;
        _lr.startWidth = 0.12f;
        _lr.endWidth = 0.04f;
    }

    public Vector3[] Plot(Vector3 pos, Vector3 force, int steps)
    {
        characterAim = false;
        bounceCount = 0;
        Vector3[] results = new Vector3[steps];
        float timeStep = Time.fixedDeltaTime;

        Vector3 moveStep = force * timeStep;

        for (int i = 0; i < steps; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(pos, moveStep, out hit, moveStep.magnitude, nonPlayer))
            {
                // Debug.Log($"Raycast hit: {hit.collider.gameObject.name} with tag {hit.collider.gameObject.tag}");
                if (hit.collider.CompareTag("bouncy"))
                {
                    characterAim = false;
                    moveStep = Vector3.Reflect(moveStep, hit.normal);
                    Debug.DrawLine(hit.point, force, Color.red);
                    bounceCount++;
                }
                else if (hit.collider.gameObject.CompareTag("enemy"))
                {
                    characterAim = true;
                    _lr.startColor = _c3; 
                    tempVec = pos;
                    return results;
                    
                }
                else if (hit.collider.gameObject.CompareTag("ground") || hit.collider.gameObject.CompareTag("MovingPlatform"))
                {
                    characterAim = false;
                    _lr.startColor = _c1; 
                    tempVec = pos;
                    return results;
                }
                
            }
            pos += moveStep;
            results[i] = pos;
        }

        if (bounceCount > 0)
        {
            _lr.startColor = _c2;
        }
        else
        {
            _lr.startColor = _c1;
        }

        tempVec = pos;

        return results;
    }

    public void RenderTrajectory(Vector3[] trajectory)
    {
        int length = 0;
        for (int i = 0; i < trajectory.Length; i++)
        {
            if (trajectory[i] != Vector3.zero)
            {
                length++;
            }
        }

        Vector3[] temp = new Vector3[length];
        int tempindex = 0;
        for (int i = 0; i < trajectory.Length; i++)
        {
            if (trajectory[i] != Vector3.zero)
            {
                temp[tempindex++] = trajectory[i];
            }
        }

        _lr.positionCount = length;
        _lr.SetPositions(temp);
    }

    public void EndLine02()
    {
        _lr.positionCount = 0;
    }
    
}
