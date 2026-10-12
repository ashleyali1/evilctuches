using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4;

    private int scoreVal = 0;
    private int ratStreak = 0;

    public TextMeshProUGUI scoreBox;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-transform.up * speed * Time.deltaTime);
        }

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -4f, 4f),
            transform.position.z
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            ProjectileMove projectile =
                collision.GetComponent<ProjectileMove>();

            if (projectile != null)
            {
                scoreVal += projectile.points;

                if (projectile.points > 0)
                {
                    ratStreak++;

                    // Bonus at streaks of 3, 6, 9, and so on.
                    if (ratStreak % 3 == 0)
                    {
                        scoreVal += 200;
                    }
                }
                else if (projectile.points < 0)
                {
                    ResetRatStreak();
                }

                UpdateScoreText();
            }

            Destroy(collision.gameObject);
        }
    }

    public void ResetRatStreak()
    {
        ratStreak = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreBox.text = "Score: " + scoreVal
            + "\nRat streak: " + ratStreak;
    }
}