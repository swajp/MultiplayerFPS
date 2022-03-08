using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    private void Update()
    {
        ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
        if (isReloading)
        {
            ammoText.text = "RELOADING";
        }
    }
    public override void Use()
    {
        Debug.Log("Using gun" + itemInfo.itemName);
        Debug.Log(currentAmmo);
        Shoot();
    }
    void Shoot()
    {
        ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
        if (isReloading)
        {
            return;
        }
        if(Time.time >= nextTimeToFire)
        {
            PV.RPC("RPC_PlaySound", RpcTarget.All, null);
            nextTimeToFire = Time.time + 1f / fireRate;
            currentAmmo--;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ray.origin = cam.transform.position;

            if (Physics.Raycast(ray, out RaycastHit hit) && currentAmmo >= 0)
            {
                Debug.Log("We hit " + hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
                PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
            }
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
        }

    }
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Debug.Log(hitPosition);
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {
            GameObject bulletImpactObj =Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f ,Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            Destroy(bulletImpactObj, 10f);
            bulletImpactObj.transform.SetParent(colliders[0].transform);
        }
    }
    [PunRPC]
    public void RPC_PlaySound()
    {
        gunSounds.PlayOneShot(gunShot);
    }
}
