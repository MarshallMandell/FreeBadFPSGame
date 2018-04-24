using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomFPS_TPS_Hybrid : UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl
{
    public Player1 player1;
    public Player2 player2;
    public Text PlayerAmmoText;
    public Text Player1HealthText;
    public Text Player2HealthText;
    public Text WinnerText;

    public Transform bulletSpawn;
    public GameObject playerGun;
    public GameObject bulletPrefab;

    public float PlayerAmmo;
    public float firingTimer;
    public float firingDelay;
    public float playerHealth;

    public bool PlayerIsReloading;
    public bool Gameover;

    public AudioSource AS;
    public AudioClip WinSound;
    public AudioClip GunShoot;
    public AudioClip ReloadSound;

    [SerializeField] public UnityStandardAssets.Characters.FirstPerson.MouseLook m_MouseLook;

    // Use this for initialization
    void Start () {
        Debug.Log("START");
        //Gameover = false;
        //WinnerText.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("UPDATE");

        firingTimer += Time.deltaTime;

        if (Gameover == true)
        {
            AS.PlayOneShot(WinSound);
            WinnerText.enabled = true;
        }
    }

    public virtual IEnumerator GameOver()
    {
        Player1HealthText.enabled = false;
        Player2HealthText.enabled = false;
        Gameover = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Additive);
        yield return new WaitForSeconds(3);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            playerHealth -= 5;

        }
    }

    public void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Cam.transform);
    }

    public virtual void Fire()
    {
        if (PlayerAmmo > 0)
        {
            var bullet = (GameObject)Instantiate(

                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);
            PlayerAmmo--;
            AS.PlayOneShot(GunShoot);
            
            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = playerGun.transform.forward * 32;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);
        }
    }
}
