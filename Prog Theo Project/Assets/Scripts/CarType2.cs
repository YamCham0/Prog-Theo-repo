using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarType2 : CarBase
{
    private int collisionCount = 0;
    private GameObject subaruuGameObject;

    void Start()
    {
        subaruuGameObject = transform.Find("Subaruu_Impreeza_WRC").gameObject;
        collisionCount = 0;
    }

    public override void Move()
    {
        // Prevent moving while in the air
        if (isJumping)
        {
            return;
        }

        base.Move(); // Polymorphism: This overrides the Move behavior with an additional check
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collisionCount++;
            PlayCrashSound(); // Polymorphism: Extends base PlayCrashSound with additional logic

            if (collisionCount == 1)
            {
                StartCoroutine(FlashCar()); // Polymorphism: Overrides base FlashCar behavior
            }

            if (collisionCount >= 2)
            {
                TriggerGameOverEffects(); // Polymorphism: Use of base class method in extended context
                TriggerGameOver(); // Polymorphism: Use of base class method in extended context
            }
        }
        else
        {
            base.OnCollisionEnter(collision); // Polymorphism: Calls the base class implementation
        }
    }

    protected override IEnumerator FlashCar()
    {

        for (int i = 0; i < 5; i++)
        {
            subaruuGameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);

            subaruuGameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        Move();
        Jump();
    }
}
