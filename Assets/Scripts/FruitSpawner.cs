using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetAreaSprite; // The 2D Sprite defining the area

    [SerializeField]
    GameObject fruit;


    void Start()
    {
        StartCoroutine(RandomFruitSpawn());
    }


    IEnumerator RandomFruitSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0, 5));
            SpawnRandomObject();
            yield return null;
        }
    }

    public void SpawnRandomObject()
    {
        Bounds bounds = targetAreaSprite.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 randomPosition = new Vector3(randomX, randomY, targetAreaSprite.transform.position.z);

        Instantiate(fruit, randomPosition, Quaternion.identity);
    }
}
