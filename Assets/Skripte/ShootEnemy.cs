using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    private bool readyToShoot = true;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootForce;
    private GameObject projectile;
    public AudioSource audioSource;
    public float shootUpwardForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToShoot)
            Shoot();
    }

    private void Shoot()
    {
        readyToShoot = false;
        projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector3 shotDirection = shootPoint.forward;
        projectile.transform.forward = shotDirection.normalized;
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(shotDirection.normalized * shootForce, ForceMode.Impulse);
        projectileRb.AddForce(shootPoint.transform.up * shootUpwardForce, ForceMode.Impulse);

        Invoke(nameof(ResetShot), 2f);

    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
}
