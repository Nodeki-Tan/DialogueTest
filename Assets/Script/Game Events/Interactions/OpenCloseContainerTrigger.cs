using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OpenCloseContainerTrigger : Interaction
{
    public Transform fromPos;  // The transform from where we interact in case you want to offset it
    public Transform toPos;  // The transform to where we interact in case you want to offset it
    public float gizmoRadius = 0.25f;               // How close do we need to be to interact?

    public GameObject toMove;       // What object to move

    public Color fromColor = Color.yellow;
    public Color toColor = Color.green;

    public float moveSpeed = 10;

    bool open = false;

    Coroutine animCoroutine;

    public override void DoSomething()
    {
        if (toMove == null)
            toMove = gameObject;

        var player = GameObject.FindGameObjectWithTag("Player");

        if (!open)
        {
            Open();

            Debug.Log("opening");
        }
        else
        {
            Close();

            Debug.Log("closing");
        }

    }

    IEnumerator doRotationOpen()
    {
        open = true;

        float time = 0;

        while (time <= 1)
        {
            yield return null;

            time += Time.deltaTime * moveSpeed;

            toMove.transform.position = Vector3.Lerp(toMove.transform.position, toPos.position, time);
        }

    }

    IEnumerator doRotationClose()
    {

        open = false;

        float time = 0;

        while (time <= 1)
        {
            yield return null;

            time += Time.deltaTime * moveSpeed;

            toMove.transform.position = Vector3.Lerp(toMove.transform.position, fromPos.position, time);
        }

    }

    public void Open()
    {
        if (!open)
        {
            if (animCoroutine != null)
            {
                StopCoroutine(animCoroutine);
            }

            animCoroutine = StartCoroutine(doRotationOpen());
        }
    }

    public void Close()
    {
        if (open)
        {
            if (animCoroutine != null)
            {
                StopCoroutine(animCoroutine);
            }

            animCoroutine = StartCoroutine(doRotationClose());
        }

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
