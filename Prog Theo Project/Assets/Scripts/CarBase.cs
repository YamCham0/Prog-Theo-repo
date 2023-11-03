using System.Collections;
using UnityEngine;

public abstract class CarBase : MonoBehaviour
{
    [SerializeField] private string carName; // Encapsulation: carName is now a serialized private field
    private int currentLaneIndex = 1; // Encapsulation: not exposed to child classes directly
    [SerializeField] protected float jumpForce = 5f; // Protected: allows access to child classes while encapsulating from outside
    protected bool isJumping = false;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    private AudioSource playerSound;
    private ParticleSystem explosionParticleInstance;

    void Awake()
    {
        playerSound = GetComponent<AudioSource>();
        explosionParticleInstance = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        explosionParticleInstance.transform.SetParent(transform);
        explosionParticleInstance.gameObject.SetActive(false);
    }

    public virtual void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && GameStateManager.Instance.isGameOver == false)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            playerSound.PlayOneShot(jumpSound, 3);
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Track"))
        {
            isJumping = false;

        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {

            PlayCrashSound();
            TriggerGameOverEffects();
            TriggerGameOver();
        }
    }

    protected void PlayCrashSound()
    {

        if (playerSound != null && crashSound != null)
        {
            playerSound.PlayOneShot(crashSound, 5);
        }
    }

    protected void TriggerGameOverEffects()
    {
        explosionParticleInstance.transform.position = transform.position;
        explosionParticleInstance.gameObject.SetActive(true);
        explosionParticleInstance.Play();
    }

    protected virtual IEnumerator FlashCar()
    {

        for (int i = 0; i < 5; i++)
        {
            gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);

            gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

    }

    protected void TriggerGameOver()
    {

        int finalScore = PlayerDataHandler.Instance.PlayerScore;
        string player = PlayerDataHandler.Instance.PlayerName;


        GameStateManager.Instance?.GameOver(finalScore, player);
    }

    public virtual void SwitchLane(bool moveRight)
    {
        Vector3 targetPosition = transform.position;
        float[] lanePositions = { -1.3f, 0f, 1.3f };

        if (moveRight && currentLaneIndex < lanePositions.Length - 1)
        {
            currentLaneIndex++;
        }
        else if (!moveRight && currentLaneIndex > 0)
        {
            currentLaneIndex--;
        }

        targetPosition.x = lanePositions[currentLaneIndex];
        transform.position = targetPosition; // Inheritance: Child classes inherit this method and can override it
    }

    public virtual void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && GameStateManager.Instance.isGameOver == false)
        {
            SwitchLane(false); // Polymorphism: Subclasses can change how this method works
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && GameStateManager.Instance.isGameOver == false)
        {
            SwitchLane(true);
        }
    }
}
