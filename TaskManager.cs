using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TaskManager : Singleton<TaskManager>
{
    public List<string> enumerators;
    public short workingTaskIndex;

    private void Awake()
    {
        byte s = 16;
    }
    private IEnumerator Start()
    {

        enumerators.Add("WaitForXray");
        enumerators.Add("WaitForDrop");
        foreach (var item in enumerators)
        {
            yield return StartCoroutine(item);
        }
        for (int i = 0; i < enumerators.Count; i++)
        {
            workingTaskIndex = (short)i;
            yield return StartCoroutine(enumerators[i]);
        }

    }

    public IEnumerator WaitForXray()
    {
        //while (true)
        //{
        //    yield return new WaitForSeconds(5f);
        //}
        Debug.Log("Wait for xray " + Time.time);
        yield return new WaitForSeconds(1f);
        Debug.Log("Wait for xray " + Time.time);
    }
    public IEnumerator WaitForDrop()
    {
        Debug.Log("Wait for drop " + Time.time);
        yield return new WaitForSeconds(1f);
        Debug.Log("Wait for drop " + Time.time);
    }
}
