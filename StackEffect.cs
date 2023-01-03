using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class StackEffect : MonoBehaviour
{
    public Vector3 vector;
    public Vector3 range;
    public float distance;
    public float lerpValue;

    public StackController stackController;

    public Transform followTarget;
    public Transform parent;
    public Transform createdObject;

    public int startWithIndex;
    private void OnEnable()
    {
        createdObject = new GameObject("Created").transform;
        createdObject.parent = parent.parent;
        createdObject.localPosition = parent.localPosition;
        followTarget = createdObject;
    }
    void FixedUpdate()
    {
        //if (stackController.isProgressing) { return; }
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            if (i <= startWithIndex)
            {
                if (followTarget)
                {
                    //child.transform.position = Vector3.Lerp(child.transform.position, followTarget.position, 1f);
                    Vector3 position = followTarget.position;
                    position.y += distance * i * range.y;
                    child.transform.position = position;

                }
                else
                {
                    Vector3 parent = Vector3.zero;
                    parent.y = child.parent.localPosition.y;
                    Debug.Log(parent);
                    child.transform.localPosition = Vector3.Lerp(child.transform.localPosition, parent + vector, lerpValue);
                }
            }
            else
            {
                Transform previousChild = parent.transform.GetChild(i - 1);
                Vector3 movementVector = previousChild.transform.localPosition;
                if (!stackController.isProgressing)
                {
                    movementVector.x += distance * range.x;
                    movementVector.y += distance * range.y;
                    movementVector.z += distance * range.z;
                    child.transform.localPosition = Vector3.Lerp(child.transform.localPosition, movementVector, lerpValue);
                }
                else
                {
                    //movementVector = Vector3.zero;
                    movementVector.y += distance * range.y;
                    child.transform.localPosition = Vector3.MoveTowards(child.transform.localPosition, movementVector, lerpValue);
                }
                //Debug.Log(Vector3.Lerp(child.transform.localPosition, movementVector, 0.2f));
                //child.transform.localPosition = movementVector;
                //child.transform.localPosition = Vector3.Lerp(child.transform.localPosition, Vector3.right, 0.2f);
                //child.transform.localPosition = Vector3.Lerp(child.transform.localPosition, Vector3.right, .3f);
            }
        }
    }
}
