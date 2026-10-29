using UnityEngine;

public class ZigzagShip : MonoBehaviour
{
    public float speed = 3f;
    public float zigzagSpeed = 4f;
    public float zigzagWidth = 1.5f;
    public GameObject explosion;

    private float startX;
    private float timer = 0f;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float x = startX + Mathf.Sin(timer * zigzagSpeed) * zigzagWidth;
        float y = transform.position.y - speed * Time.deltaTime;

        transform.position = new Vector3(x, y, transform.position.z);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("GalagaPlayer"))
        {
             if (collision.gameObject.CompareTag("PlayerBullet"))

                Destroy(collision.gameObject);

            if (explosion != null)
            {
                GameObject temp = Instantiate(
                    explosion,
                    transform.position,
                    Quaternion.identity
                );

                Destroy(temp, 1.0f);
            }

            Destroy(gameObject);
        }
    }
}
