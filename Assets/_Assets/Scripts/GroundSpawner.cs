using System.Collections;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;  // Prefab c?a Ground
    public float spawnRate = 2f;     // T?n su?t spawn (sau m?i bao l�u)
    public float minX = -10f;       // V? tr� X t?i thi?u
    public float maxX = 10f;        // V? tr� X t?i ?a
    public float minY = -2f;        // V? tr� Y t?i thi?u
    public float maxY = 2f;         // V? tr� Y t?i ?a
    public int maxGrounds = 10;     // S? l??ng Ground t?i ?a
    private float groundWidth;      // ?? r?ng c?a ground prefab

    void Start()
    {
        // L?y chi?u r?ng c?a Ground prefab (gi? s? ??i t??ng Ground c� BoxCollider2D)
        groundWidth = groundPrefab.GetComponent<Collider2D>().bounds.size.x;
        // B?t ??u spawn Ground
        StartCoroutine(SpawnGrounds());
    }

    IEnumerator SpawnGrounds()
    {
        while (true)
        {
            // Ch? m?t kho?ng th?i gian nh?t ??nh tr??c khi spawn th�m
            yield return new WaitForSeconds(spawnRate);

            // Random h�a v? tr� spawn c?a Ground
            float spawnX = Random.Range(minX, maxX);
            float spawnY = Random.Range(minY, maxY);

            // Ki?m tra n?u ?ang c� qu� nhi?u Ground trong scene
            if (transform.childCount < maxGrounds)
            {
                // Spawn Ground m?i t?i v? tr� random
                GameObject ground = Instantiate(groundPrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
                ground.transform.parent = transform;  // ?? d? qu?n l�, g�n l�m con c?a ??i t??ng n�y
            }
        }
    }
}