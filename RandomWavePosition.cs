using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RandomWavePosition : MonoBehaviour
{
    public GameObject customerPrefab;

    public float minX, MaxX;
    public float minZ, MaxZ;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            int pos = 0;
            float random = Random.Range(0f, 1f);
            if (random > 0.5f) { pos = 1; }
            Vector3 position = Vector3.one;
            if (pos == 0)
            {
                position.x = Random.Range(minX, MaxX);
                position.z = Random.Range(0f, 1f) > 0.5f ? minZ : MaxZ;
            }
            else
            {
                position.z = Random.Range(minZ, MaxZ);
                position.x = Random.Range(0f, 1f) > 0.5f ? minX :MaxX;
            }
            transform.position = position;
        }
    }
}
