using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Camera playerCamera;
    public Animator gunAnimator;
    public AudioSource gunSound;

    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 0.3f;

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (gunAnimator != null)
        {
            gunAnimator.Play("gunshot", 0, 0f);
        }

        if (gunSound != null)
        {
            gunSound.Play();
        }

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Pataikei i: " + hit.collider.name);

            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}