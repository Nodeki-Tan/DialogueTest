using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpenType
{

    alwaysout = 0,

    forward = 1,

    inwards = -1

}

public class DoorTrigger : Interaction
{
    public GameObject toMove;       // What object to move

    public float rotSpeed = 10;
    float rotAmount = 90;
    bool moving = false;
    float fraction = 0;

    float forwardDirection;
    public OpenType openMethod;

    public bool open = false;

    Vector3 StartRotation;
    Vector3 forward;

    Coroutine animCoroutine;


    private void Awake()
    {
        StartRotation = toMove.transform.eulerAngles;
        forward = toMove.transform.forward;

        forwardDirection = (float)openMethod;
    }

    public override void DoSomething()
    {
        if (toMove == null)
            toMove = gameObject;

        var player = GameObject.FindGameObjectWithTag("Player");

        if (!open)
        {
            Open(player);

            Debug.Log("opening");
        }
        else
        {
            Close(player);

            Debug.Log("closing");
        }

    }

    IEnumerator doRotationOpen(float value)
    {
        Quaternion startRotation = toMove.transform.rotation;
        Quaternion endRotation;

        if (value >= forwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + rotAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - rotAmount, 0));
        }

        open = true;

        float time = 0;

        while (time <= 1)
        {
            yield return null;

            time += Time.deltaTime * rotSpeed;

            toMove.transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
        }

    }

    IEnumerator doRotationClose(float value)
    {
        Quaternion startRotation = toMove.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        open = false;

        float time = 0;

        while (time <= 1)
        {
            yield return null;

            time += Time.deltaTime * rotSpeed;

            toMove.transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
        }

    }

    public void Open(GameObject player)
    {
        if (!open)
        {
            if (animCoroutine != null)
            {
                StopCoroutine(animCoroutine);
            }

            float dot = Vector3.Dot(forward, (player.transform.position - toMove.transform.position).normalized);

            Debug.Log(dot);

            animCoroutine = StartCoroutine(doRotationOpen(dot));
        }
    }

    public void Close(GameObject player)
    {
        if (open)
        {
            if (animCoroutine != null)
            {
                StopCoroutine(animCoroutine);
            }

            float dot = Vector3.Dot(forward, (player.transform.position - toMove.transform.position).normalized);

            Debug.Log(dot);

            animCoroutine = StartCoroutine(doRotationClose(dot));
        }

    }

}
