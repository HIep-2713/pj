using System.Collections;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;  // Prefab c?a Ground
    public float spawnRate = 2f;     // T?n su?t spawn (sau m?i bao lâu)
    public float minX = -10f;       // V? trí X t?i thi?u
    public float maxX = 10f;        // V? trí X t?i ?a
    public float minY = -2f;        // V? trí Y t?i thi?u
    public float maxY = 2f;         // V? trí Y t?i ?a
    public int maxGrounds = 10;     // S? l??ng Ground t?i ?a
    private float groundWidth;      // ?? r?ng c?a ground prefab

    void Start()
    {
        // L?y chi?u r?ng c?a Ground prefab (gi? s? ??i t??ng Ground có BoxCollider2D)
        groundWidth = groundPrefab.GetComponent<Collider2D>().bounds.size.x;
        // B?t ??u spawn Ground
        StartCoroutine(SpawnGrounds());
    }

    IEnumerator SpawnGrounds()
    {
        while (true)
        {
            // Ch? m?t kho?ng th?i gian nh?t ??nh tr??c khi spawn thêm
            yield return new WaitForSeconds(spawnRate);

            // Random hóa v? trí spawn c?a Ground
            float spawnX = Random.Range(minX, maxX);
            float spawnY = Random.Range(minY, maxY);

            // Ki?m tra n?u ?ang có quá nhi?u Ground trong scene
            if (transform.childCount < maxGrounds)
            {
                // Spawn Ground m?i t?i v? trí random
                GameObject ground = Instantiate(groundPrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
                ground.transform.parent = transform;  // ?? d? qu?n lý, gán làm con c?a ??i t??ng này
            }
        }
    }
}