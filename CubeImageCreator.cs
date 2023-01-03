using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

public class CubeImageCreator : MonoBehaviour
{
    [SerializeField] Texture2D texture;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int scanningRange;
    [SerializeField] private string materialsFolderPath;
    
    private readonly List<Material> materials = new List<Material>();

    private Material material;

    private int width, height;

    [Button("Create Cubes")] // Import OdinInspector or comment out this row and run CreateCubes method in another way
    private void CreateCubes()
    {
        ClearCubes();
        
        width = texture.width;
        height = texture.height;

        int index = 0;
        
        for (int i = 0; i < width; i += scanningRange)
        {
            for (int j = 0; j < height; j += scanningRange)
            {
                if (texture.GetPixel(i, j).a == 0) continue; // When the pixel alpha value is equal to zero, the next step is passed.

                int x = i == 0 ? i : i / scanningRange;
                int z = j == 0 ? j : j / scanningRange;

                Material tempMaterial;
                Material newMaterial;
                
                if (CheckColorContainsInMaterials(texture.GetPixel(i, j), out tempMaterial)) // If another previously created material in the materials has the same color, then it is used.
                {
                    newMaterial = tempMaterial;
                }
                else
                {
                    newMaterial = new Material(Shader.Find("Unlit/Color")) // Create a new material and save the color
                    {
                        name = $"{texture.name}{index}",
                        color = texture.GetPixel(i, j),
                    };
                    materials.Add(newMaterial);
                }
                
                GameObject newCube = (GameObject)PrefabUtility.InstantiatePrefab(cubePrefab, transform); // Clones the object without detaching it from the prefab
                newCube.transform.position = new Vector3(x, 0, z);
                newCube.GetComponent<Renderer>().material = newMaterial;

                index++;
            }
        }
    }

    [Button("Save Materials to Assets")] // Import OdinInspector or comment out this row and run SaveMaterials method in another way
    private void SaveMaterials()
    {
        string guid = AssetDatabase.CreateFolder("Assets", $"{texture.name}Materials");
        materialsFolderPath = AssetDatabase.GUIDToAssetPath(guid);

        foreach (Material mat in materials)
        {
            string newAssetPath = $"{materialsFolderPath}/{mat.name}.mat";

            AssetDatabase.CreateAsset(mat, newAssetPath);
            AssetDatabase.SaveAssets();
        }
    }

    [Button("Clear Cubes")] // Import OdinInspector or comment out this row and run ClearCubes method in another way
    private void ClearCubes()
    {
        materials.Clear();
        
        AssetDatabase.DeleteAsset(materialsFolderPath);
        AssetDatabase.SaveAssets();
        
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private bool CheckColorContainsInMaterials(Color color, out Material outMaterial)
    {

        foreach (Material mat in materials)
        {
            if (mat.color != color) continue;
            
            outMaterial = mat;
            return true;
        }

        outMaterial = null;
        return false;
    }
}