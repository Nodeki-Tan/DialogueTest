using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTrigger : Interaction
{

    public Transform fromPos;  // The transform from where we interact in case you want to offset it
    public Transform toPos;  // The transform to where we interact in case you want to offset it
    public float gizmoRadius = 0.25f;               // How close do we need to be to interact?

    public GameObject toMove;       // What object to move

    public Color fromColor = Color.yellow;
    public Color toColor = Color.green;

    public float moveSpeed = 10;
    bool moving = false;
    float fraction = 0;

    public override void DoSomething()
    {
        if (toMove == null)
            toMove = gameObject;


        MoveTo();
    }

    private void Update()
    {
        if (moving)
        {
            if (fraction <= Vector3.Distance(fromPos.position, toPos.position))
            {
                fraction += Time.deltaTime * moveSpeed;
                toMove.transform.position = Vector3.Lerp(fromPos.position, toPos.position, fraction);
            }
            else
            {
                moving = false;
            }
        }
    }

    void MoveTo()
    {
        moving = true;
    }

    void OnDrawGizmosSelected()
    {
        if (fromPos == null)
            fromPos = transform;

        Gizmos.color = fromColor;
        Gizmos.DrawWireSphere(fromPos.position, gizmoRadius);

        Gizmos.color = toColor;
        Gizmos.DrawWireSphere(toPos.position, gizmoRadius);
    }
}
