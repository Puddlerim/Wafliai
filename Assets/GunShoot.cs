using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Camera playerCamera;

    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 0.3f;

    public Animator gunAnimator;

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
        // Paleidžia šūvio animaciją nuo pradžios
        if (gunAnimator != null)
        {
            gunAnimator.Play("gunshot", 0, 0f);
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
        else
        {
            Debug.Log("Suvis, bet nepataikei.");
        }
    }
}