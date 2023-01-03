using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class StackableStair : MonoBehaviour
{
    public StairSettings stairSettings;
    [Range(1, 10)]

    public int stairAmount;
    public Vector3 offset;

    private List<GameObject> stairList;
    private void OnValidate()
    {
        stairList = new List<GameObject>();

        DestroyAll();
        GameObject stairPiece;
        Vector3 vector = Vector3.zero;
        for (int i = 0; i < stairAmount; i++)
        {
            stairPiece = Instantiate(stairSettings.stairPiecePrefab, transform);
            vector.y = i * offset.y;
            stairPiece.transform.localPosition = vector;
            stairList.Add(stairPiece);
        }
    }
    public void DestroyAll()
    {
        var tempList = transform.Cast<Transform>().ToList();
        foreach (var child in tempList)
        {
            if (Application.isPlaying)
            {

                Destroy(child.gameObject);
            }
            else
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    if (child)
                    {
                        DestroyImmediate(child.gameObject);
                    }
                };
            }
        }
        //stairList.ForEach(x => Destroy(x));
        if (stairList.Count > 0)
            stairList.Clear();
    }
}
