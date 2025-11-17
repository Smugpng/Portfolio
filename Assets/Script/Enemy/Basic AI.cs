using UnityEngine;

public class BasicAI : MonoBehaviour
{

    [Header("Location Params")]
    [SerializeField] private GameObject max, min;
    private Vector3 minPos, maxPos,targetPos;
    [SerializeField] private LayerMask wallMask;

    [Header("AI States")]
    private bool hasTarget,checking;
    [SerializeField] private Vector3 currentVel;
    [SerializeField] private float smoothTime;

    [Header("OBS Avoidance")]
    private Vector3 HitPoint;
    private bool obsInWay;
    [SerializeField] private float timer;

    private void Awake()
    {
        minPos = min.transform.position;
        maxPos = max.transform.position;
        FindPosition();
    }
    private void Update()
    {
        //Avoidance Casting
        /*
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 3f, transform.forward, out hit, wallMask))
        {
            Debug.LogWarning("Wall in Way!");
            HitPoint = hit.point;
            obsInWay = true;
        }
        else
        {
            HitPoint = this.transform.position;
            obsInWay = false; 
        }
        if(obsInWay) transform.position = Vector3.MoveTowards(transform.position, HitPoint, -1 * smoothTime * Time.deltaTime);
        */

        if (hasTarget)
        {
            float dist = Vector3.Distance(transform.position, targetPos);
            if(dist <= 2)
            {
                hasTarget = false;
                timer = 0;
                Invoke("FindPosition", 2);
            }
            else
            {
                if(timer <= 10)
                {
                    transform.LookAt(targetPos);
                    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVel, smoothTime);
                    timer = timer + Time.deltaTime;
                }
                else
                {
                    hasTarget = false;
                    timer = 0;
                    Invoke("FindPosition", .5f);
                    timer = 0;
                }
                

            }
        }
        else if(!hasTarget && !checking)
        {
            checking = true;
            Invoke("FindPosition", 2);
        }
       

    }
    private void FindPosition()
    {
        float x = Random.Range(minPos.x, maxPos.x);
        float y = Random.Range(minPos.y, maxPos.y);
        float z = Random.Range(minPos.z, maxPos.z);

        Vector3 randomPos = new Vector3(x, y, z);
        targetPos = randomPos;
        MoveCheck();
    }
    private void MoveCheck()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, targetPos, out hit, Mathf.Infinity, wallMask))
        {
            FindPosition();
            Debug.Log("Wall Hit Relooking");
        }
        else
        {
            Debug.Log("WallNotHit");
            checking = false;
            hasTarget = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetPos, 4f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPos);
        Gizmos.DrawSphere(HitPoint, 2f);
    }
}
