using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
public class Player1 : CustomFPS_TPS_Hybrid
{
    // Use this for initialization
    void Start() {
        m_MouseLook.yRot = Input.GetAxis("Mouse X") * m_MouseLook.XSensitivity;
        m_MouseLook.xRot = Input.GetAxis("Mouse Y") * m_MouseLook.YSensitivity;
        Player1HealthText.enabled = true;
        PlayerAmmo = 30;
        playerHealth = 100;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log("Update Loop Player1");
        firingTimer += Time.deltaTime;

        RotateView();

        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        Player1HealthText.text = playerHealth + "/100";
        if ((Input.GetMouseButton(0) && firingTimer >= firingDelay) && Gameover == false)
        {
            Fire();
            firingTimer = 0;
        }

        if ((Input.GetKey("r") && PlayerAmmo < 29) && PlayerIsReloading == false)
        {
            StartCoroutine(ReloadAmmo());
        }

        if (playerHealth <= 0 && Gameover == false)
        {
            WinnerText.text = "Player 2 Wins!";
            StartCoroutine(GameOver());
        }

    }

    public IEnumerator ReloadAmmo()
    {

        PlayerIsReloading = true;
        AS.PlayOneShot(ReloadSound);
        yield return new WaitForSeconds(3);
        PlayerAmmo = 30;
        PlayerIsReloading = false;
        PlayerAmmoText.text = PlayerAmmo + "/30";
    }

    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.LeftControl);

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        m_Character.Move(m_Move, crouch, m_Jump, h);
        m_Jump = false;
    }

    public override void Fire()
    {
        PlayerAmmoText.text = PlayerAmmo + "/30";

        base.Fire();
    }

    public override IEnumerator GameOver()
    {
        return base.GameOver();
    }
}

