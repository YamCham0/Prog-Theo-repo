using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstraction: Abstract Car class that serves as the base for all car types
public abstract class CarBase : MonoBehaviour
{
    public string carName;
    public float laneSwitchSpeed = 2.0f;
    private int currentLaneIndex = 1;
    [SerializeField] protected float jumpForce = 5f;
    public bool isJumping = false;
    public ParticleSystem explosionParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerSound;
    private ParticleSystem explosionParticleInstance;
    private GameObject subaruuGameObject;

    public void Start()
    {
        Debug.Log("CarBase Start method called.");

        subaruuGameObject = transform.Find("Subaruu_Impreeza_WRC").gameObject;
        playerSound = GetComponent<AudioSource>();

        explosionParticleInstance = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        explosionParticleInstance.transform.SetParent(transform);  // Make it a child of this gameObject
        explosionParticleInstance.gameObject.SetActive(false);  // Initially set to inactive
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



    // OnCollisionEnter to detect landing
    public virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CarBase OnCollisionEnter called.");
        if (collision.gameObject.CompareTag("Track"))
        {
            isJumping = false;
            Debug.Log("Landed back on: " + collision.gameObject.name);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision Detected with: " + collision.gameObject.name);
            PlayCrashSound();
            TriggerGameOverEffects();
            TriggerGameOver();
        }
    }

    protected void PlayCrashSound()
    {
        playerSound.PlayOneShot(crashSound, 5);
    }

    protected void TriggerGameOverEffects()
    {
        explosionParticleInstance.transform.position = transform.position;
        explosionParticleInstance.gameObject.SetActive(true);
        explosionParticleInstance.Play();
    }

    protected IEnumerator FlashCar()
{
    Debug.Log("FlashCar coroutine started.");
    for (int i = 0; i < 5; i++)
    {
        Debug.Log("Flashing off.");
        subaruuGameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        Debug.Log("Flashing on.");
        subaruuGameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }
    Debug.Log("FlashCar coroutine ended.");
}

    protected void TriggerGameOver()
    {
        int finalScore = PlayerDataHandler.Instance.PlayerScore;
        string player = PlayerDataHandler.Instance.PlayerName;

        Debug.Log("TriggerGameOver - Final Score: " + finalScore + ", Player: " + player);

        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.GameOver(finalScore, player);
        }
        else
        {
            Debug.LogError("GameStateManager instance is null");
        }
    }




    // Function to switch lanes left or right
    // Initialize to 1 for the middle lane
    public virtual void SwitchLane(bool moveRight)
    {
        Vector3 targetPosition = transform.position;

        // Define lane positions
        float[] lanePositions = { -1.3f, 0f, 1.3f };

        // Move to adjacent lane if possible
        if (moveRight && currentLaneIndex < lanePositions.Length - 1)
        {
            currentLaneIndex++;
        }
        else if (!moveRight && currentLaneIndex > 0)
        {
            currentLaneIndex--;
        }

        // Set the new x position
        targetPosition.x = lanePositions[currentLaneIndex];
        transform.position = targetPosition;
    }

    public virtual void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && GameStateManager.Instance.isGameOver == false)
        {
            SwitchLane(false);  // Move left
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && GameStateManager.Instance.isGameOver == false)
        {
            SwitchLane(true);  // Move right
        }
    }
}
