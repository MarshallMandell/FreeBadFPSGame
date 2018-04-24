using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
public class Player2 : CustomFPS_TPS_Hybrid
{
    // Use this for initialization
    void Start()
    {
        Gameover = false;
        WinnerText.enabled = false;
        Player2HealthText.enabled = true;
        PlayerAmmo = 30;
        playerHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1))
            m_MouseLook.yRot = -0.75f * m_MouseLook.XSensitivity;

        if (Input.GetKey(KeyCode.Keypad3))
            m_MouseLook.yRot = 0.75f * m_MouseLook.XSensitivity;

        if (Input.GetKey(KeyCode.Keypad0))
            m_MouseLook.xRot = -0.75f * m_MouseLook.YSensitivity;

        if (Input.GetKey(KeyCode.Keypad5))
            m_MouseLook.xRot = 0.75f * m_MouseLook.YSensitivity;
        firingTimer += Time.deltaTime;

        RotateView();

        if (!m_Jump)
        {
            m_Jump = Input.GetKeyDown(KeyCode.KeypadPlus);
        }
        Player2HealthText.text = playerHealth + "/100";
        if ((Input.GetKey(KeyCode.KeypadEnter) && firingTimer >= firingDelay) && Gameover == false)
        {
            Fire();
            firingTimer = 0;
        }

        if ((Input.GetKey("t") && PlayerAmmo < 29) && PlayerIsReloading == false)
        {
            StartCoroutine(ReloadAmmo());
        }

        if (playerHealth <= 0 && Gameover == false)
        {
            WinnerText.text = "Player 1 Wins!";
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
        float h = CrossPlatformInputManager.GetAxis("Horizontal2");
        float v = CrossPlatformInputManager.GetAxis("Vertical2");
        bool crouch = Input.GetKey(KeyCode.RightControl);

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
        if (Input.GetKey(KeyCode.RightShift)) m_Move *= 0.5f;
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

