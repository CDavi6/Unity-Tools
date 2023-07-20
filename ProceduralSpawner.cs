using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProceduralSpawner : MonoBehaviour
{
    public GameObject prefab;

    [Range(0, 10000)]
    public int density;

    [Header("Spawn range parameters")]
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 yRange;
    
    [Header("Prefab size/rotation")]
    public Vector3 prefabSize;
    public Vector3 prefabRotation;

    [Header("Align with ground rotation | Will ignore prefab rotation")]
    public bool alignToGroundRotation;

    public void GenerateReplace()
    {
        ClearMatching();

        for (int i = 0; i < density; i++)
        {
            float rangeX = Random.Range(xRange.x, xRange.y);
            float rangeY = Random.Range(xRange.x, yRange.y);

            // get random position on the x and z axis starting from the spawner
            Vector3 rayStart = new Vector3(rangeX, 50000, rangeY);

            // ray cast down and if the area is safe to spawn then spawn, otherwise do nothing
            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("ground"))
                {
                    GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(this.prefab, transform);
                    instantiatedPrefab.transform.position = hit.point;
                    instantiatedPrefab.transform.localScale = prefabSize;
                    instantiatedPrefab.transform.eulerAngles = prefabRotation;
                }
            }
        }
    }

    public void GenerateAdd()
    {
        for (int i = 0; i < density; i++)
        {
            float rangeX = Random.Range(xRange.x, xRange.y);
            float rangeY = Random.Range(xRange.x, yRange.y);

            // get random position on the x and z axis starting from the spawner
            Vector3 rayStart = new Vector3(rangeX, 50000, rangeY);

            // ray cast down and if the area is safe to spawn then spawn, otherwise do nothing
            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("ground"))
                {
                    GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(this.prefab, transform);
                    instantiatedPrefab.transform.position = hit.point;
                    instantiatedPrefab.transform.localScale = prefabSize;
                    instantiatedPrefab.transform.eulerAngles = prefabRotation;
                }
            }
        }
    }

    public void ClearAll()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public void ClearMatching()
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.name == prefab.name)
            {
                DestroyImmediate(obj);
            }
        }
    }
}
