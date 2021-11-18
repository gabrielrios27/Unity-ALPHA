using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGunController : MonoBehaviour
{
    [SerializeField] private GameObject bulletOrigen;
    [SerializeField] private float distanceRay = 10f;
    [SerializeField] private int shootCooldown = 2;

    [SerializeField] private float timerShoot = 0;
    [SerializeField] private GameObject bulletPrefab;

    private bool  canShoot = true;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            RaycastCannon();
        }
        else
        {
           timerShoot += Time.deltaTime;
        }

     
        if(timerShoot > shootCooldown)
        {
            canShoot = true;
        }

    }
    private void RaycastCannon()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(bulletOrigen.transform.position, bulletOrigen.transform.TransformDirection(Vector3.forward), out hit, distanceRay))
        {
            if(hit.transform.CompareTag("Player"))
            {
                Debug.Log("COLISION PLAYER");
                canShoot   = false;
                timerShoot = 0;
                Instantiate(bulletPrefab, bulletOrigen.transform.position, bulletOrigen.transform.rotation);
            }
        }

    }

    private void OnDrawGizmos()
    {

        if (canShoot) { 
            Gizmos.color = Color.blue;
            Vector3 direction = bulletOrigen.transform.TransformDirection(Vector3.forward) * distanceRay;
            Gizmos.DrawRay(bulletOrigen.transform.position, direction);
        }

    }
}
