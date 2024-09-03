using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 15f;
    public float maxDistance = 15f; // Maximum distance the bullet can travel
    private float traveledDistance = 0f;
    private float startTime;
    public Rigidbody2D rb;
    public GameObject impacteffect;
    public Vector2 moveDirection;
    public int damageAmount = 1;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        // Move the bullet
        rb.velocity = moveDirection * bulletSpeed;

        // Check if the bullet has traveled beyond its maximum distance
        if (traveledDistance >= maxDistance)
        {
            DestroyBullet();
        }

        // Update traveled distance
        traveledDistance += bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }
        if (collision.tag == "Boss")
        {
            BossHealthController.instance.TakeDamage(damageAmount);
        }
        if (impacteffect != null)
        {
            Instantiate(impacteffect, transform.position, Quaternion.identity);
        }
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
