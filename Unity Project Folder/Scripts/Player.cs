using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject projectileLvl1;
    [SerializeField]
    private GameObject projectileLvl2;
    [SerializeField]
    private GameObject projectileLvl3;
    [SerializeField]
    private GameObject projectileLvl4;
    [SerializeField]
    private GameObject shootingPoint;
    [SerializeField]
    private GameObject shootingPointLvl1;
    [SerializeField]
    private GameObject shootingPointLvl2;
    [SerializeField]
    private GameObject shootingPointLvl3;
    [SerializeField]
    private GameObject shootingPointLvl4;
    [SerializeField]
    private GameObject shieldObj;
    public int health;
    public int shield;
    [SerializeField]
    private int speed;
    public int powerLvl;
    [SerializeField]
    private float fireRate;
    private float resetFireRate;
    private bool fired;
    private AudioSource aS;
    [SerializeField]
    private AudioClip shoot;
    [SerializeField]
    private AudioClip upgrade;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        resetFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        if (powerLvl > 6)
        {
            powerLvl = 6;
        }
        if (shield > 3)
        {
            shield = 3;
        }
        else if (shield == 0)
        {
            shieldObj.SetActive(false);
        }
    }

    private void Movement()
    {
        //Basic Movement
        var x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        var y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.position += new Vector3(x, y, 0);

        //LookAt Cursor
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && !fired)
        {
            powerLvlSystem();
            aS.PlayOneShot(shoot);
        }
        if (fired)
        {
            fireRate -= Time.deltaTime;
            if (fireRate <= 0)
            {
                fireRate = resetFireRate;
                fired = false;
            }
        }
    }

    private void powerLvlSystem()
    {
        switch (powerLvl)
        {
            case 1:
                shootingPoint.SetActive(false);
                shootingPointLvl1.SetActive(true);
                shootingPointLvl2.SetActive(true);
                Instantiate(projectileLvl1, shootingPoint.transform.position, shootingPoint.transform.rotation);
                Instantiate(projectileLvl1, shootingPointLvl1.transform.position, shootingPointLvl1.transform.rotation);
                fired = true;
                break;
            case 2:
                shootingPoint.SetActive(true);
                Instantiate(projectileLvl1, shootingPoint.transform.position, shootingPoint.transform.rotation);
                Instantiate(projectileLvl1, shootingPointLvl1.transform.position, shootingPointLvl1.transform.rotation);
                Instantiate(projectileLvl1, shootingPointLvl2.transform.position, shootingPointLvl2.transform.rotation);
                fired = true;
                break;
            case 3:
                shootingPointLvl3.SetActive(true);
                shootingPointLvl4.SetActive(true);
                Instantiate(projectileLvl1, shootingPoint.transform.position, shootingPoint.transform.rotation);
                Instantiate(projectileLvl1, shootingPointLvl1.transform.position, shootingPointLvl1.transform.rotation);
                Instantiate(projectileLvl1, shootingPointLvl2.transform.position, shootingPointLvl2.transform.rotation);
                Instantiate(projectileLvl1, shootingPointLvl3.transform.position, shootingPointLvl2.transform.rotation);
                Instantiate(projectileLvl1, shootingPointLvl4.transform.position, shootingPointLvl2.transform.rotation);
                fired = true;
                break;
            case 4:
                Instantiate(projectileLvl2, shootingPoint.transform.position, shootingPoint.transform.rotation);
                Instantiate(projectileLvl2, shootingPointLvl1.transform.position, shootingPointLvl1.transform.rotation);
                Instantiate(projectileLvl2, shootingPointLvl2.transform.position, shootingPointLvl2.transform.rotation);
                Instantiate(projectileLvl2, shootingPointLvl3.transform.position, shootingPointLvl2.transform.rotation);
                Instantiate(projectileLvl2, shootingPointLvl4.transform.position, shootingPointLvl2.transform.rotation);
                fired = true;
                break;
            case 5:
                Instantiate(projectileLvl3, shootingPoint.transform.position, shootingPoint.transform.rotation);
                Instantiate(projectileLvl3, shootingPointLvl1.transform.position, shootingPointLvl1.transform.rotation);
                Instantiate(projectileLvl3, shootingPointLvl2.transform.position, shootingPointLvl2.transform.rotation);
                Instantiate(projectileLvl3, shootingPointLvl3.transform.position, shootingPointLvl2.transform.rotation);
                Instantiate(projectileLvl3, shootingPointLvl4.transform.position, shootingPointLvl2.transform.rotation);
                fireRate = 0.1f;
                fired = true;
                break;
            case 6:
                Instantiate(projectileLvl4, shootingPoint.transform.position, shootingPoint.transform.rotation);
                Instantiate(projectileLvl4, shootingPointLvl1.transform.position, shootingPointLvl1.transform.rotation);
                Instantiate(projectileLvl4, shootingPointLvl2.transform.position, shootingPointLvl2.transform.rotation);
                Instantiate(projectileLvl4, shootingPointLvl3.transform.position, shootingPointLvl2.transform.rotation);
                Instantiate(projectileLvl4, shootingPointLvl4.transform.position, shootingPointLvl2.transform.rotation);
                fireRate = 0.1f;
                fired = true;
                break;
            default:
                Instantiate(projectileLvl1, shootingPoint.transform.position, shootingPoint.transform.rotation);
                fired = true;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (shield > 0)
        {
            shield -= 1;
            Destroy(collision.gameObject);
        }
        else
        {
            health -= 1;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpaceShooterAssetPack_Miscellaneous_14(Clone)")
        {
            powerLvl += 1;
            aS.PlayOneShot(upgrade);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name == "SpaceShooterAssetPack_Miscellaneous_48(Clone)")
        {
            health += 1;
            aS.PlayOneShot(upgrade);
            Destroy(collision.gameObject);
        }
        else
        {
            shield += 1;
            aS.PlayOneShot(upgrade);
            shieldObj.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}