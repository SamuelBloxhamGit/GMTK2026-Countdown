using UnityEngine;

public class Fruit : MonoBehaviour
{

    [SerializeField]
    GameObject sprite;
    [SerializeField]
    GameObject shadowSprite;

    [SerializeField]
    Sprite[] fruitSprites;

    private void Start()
    {
        Invoke("DelayedShow", 0.1f);
    }

    void DelayedShow()
    {
        sprite.GetComponent<SpriteRenderer>().sprite = fruitSprites[Random.Range(0,fruitSprites.Length)];
        shadowSprite.GetComponent<SpriteRenderer>().sprite = sprite.GetComponent<SpriteRenderer>().sprite;
        sprite.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.UpdateCountdown(5);
            Destroy(gameObject);
        }
        else if(collision.tag == "DashBump")
        {

        }
        else if (collision.name.Contains("Powerup") || collision.tag == "Untagged")
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }
}
