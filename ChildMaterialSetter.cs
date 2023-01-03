using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class ChildMaterialSetter : MonoBehaviour
{
    public Material lockedMat;
    private Dictionary<Renderer, List<Material>> renderers = new Dictionary<Renderer, List<Material>>();
    private List<Renderer> meshRenderers = new List<Renderer>();
    private void Start()
    {
        GetDefaultMats();
    }
    public void GetDefaultMats()
    {
        meshRenderers = GetComponentsInChildren<Renderer>().ToList();

        foreach (var item in meshRenderers)
        {
            renderers.Add(item, item.materials.ToList());
        }
    }

    public void SetMaterialsToGray()
    {
        List<Material> items = new List<Material>();
        foreach (var item in meshRenderers)
        {
            items.Clear();
            
            for (int i = 0; i < item.materials.Length; i++)
            {
                items.Add(lockedMat);   
            }
            item.materials = items.ToArray();
        }
    }
    public void SetDefaultMats()
    {
        foreach (var item in meshRenderers)
        {
            item.materials = renderers[item].ToArray();
        }
    }
}
