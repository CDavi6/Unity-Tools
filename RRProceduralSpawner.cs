using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RRProceduralSpawner : MonoBehaviour
{
    public GameObject prefab;

    //public string deleteTag;

    [Header("Density | Min Inclusive - Max Exclusive")]
    public int minDensity;
    public int maxDensity;
    int density;

    [Header("Spawn range parameters")]
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 yRange;
    
    [Header("Prefab scale")]
    public int minScale;
    public int maxScale;
    int Scale;

    [Header("Prefab rotation")]
    public float minRotation;
    public float maxRotation;
    Vector3 rotation;

    [Header("Align with ground rotation | Will ignore prefab rotation")]
    public bool alignToGroundRotation;

    public void GenerateReplace()
    {
        ClearMatching();
        CalculateDensity();

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
                    CalculateScale();
                    CalculateRotation();
                    GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(this.prefab, transform);
                    instantiatedPrefab.transform.position = hit.point;
                    instantiatedPrefab.transform.localScale = Vector3.one * Scale;
                    instantiatedPrefab.transform.eulerAngles = rotation;

                    if (alignToGroundRotation)
                    {
                        instantiatedPrefab.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0)); // temp
                    }
                }
            }
        }
    }

    public void GenerateAdd()
    {
        CalculateDensity();

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
                    CalculateScale();
                    CalculateRotation();
                    GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(this.prefab, transform);
                    instantiatedPrefab.transform.position = hit.point;
                    instantiatedPrefab.transform.localScale = Vector3.one * Scale;
                    instantiatedPrefab.transform.eulerAngles = rotation;

                    if (alignToGroundRotation)
                    {
                        instantiatedPrefab.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0)); // temp
                    }
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

    public void CalculateDensity() {
        density = Random.Range(minDensity, maxDensity);
    }

    public void CalculateScale() {
        Scale = Random.Range(minScale, maxScale);
    }

    public void CalculateRotation()
    {
        rotation.x = Random.Range(minRotation, maxRotation);
        rotation.y = Random.Range(minRotation, maxRotation);
        rotation.z = Random.Range(minRotation, maxRotation);
    }
}
