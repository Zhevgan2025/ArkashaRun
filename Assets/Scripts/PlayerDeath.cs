using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Animator anim;

    [SerializeField] private GameOverUI gameOverUI;



    private bool isDead;
    [SerializeField] private float deathDelay = 1.2f;
    private bool canRestart = false;



    private void Awake()
    {
        if (!movement) movement = GetComponent<PlayerMovement>();

        if (!anim) anim = GetComponentInChildren<Animator>();

        if (!gameOverUI) gameOverUI = FindObjectOfType<GameOverUI>(true);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.collider.CompareTag("Obstacle"))
        {
            ExplodeOnHit explode = collision.collider.GetComponent<ExplodeOnHit>();
            if (explode != null)
            {
                explode.Explode();
                Destroy(collision.collider.gameObject);
            }

            Die();
        }
    }


    private void Die()
    {
        isDead = true;

        if (movement) movement.enabled = false;

        var rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            rb.constraints = RigidbodyConstraints2D.FreezePositionX
                           | RigidbodyConstraints2D.FreezeRotation;
        }

        if (anim) anim.SetTrigger("Death");

        Invoke(nameof(OnDeathFinished), deathDelay);
    }


    private void Update()
    {
        if (!canRestart) return;


        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnDeathFinished()
    {
        canRestart = true;
        if (gameOverUI) gameOverUI.Show();


        Debug.Log("GAME OVER. Press R to restart.");
    }


}
