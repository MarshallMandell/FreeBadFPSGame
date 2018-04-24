using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeIn : MonoBehaviour {

    public Image BackroundImage;
    public Text GameOverText;
    public Color TargetColor;
    private Color StartingColor;
    public float fadeSpeed = 0.05f;
    bool triggerFadeIn = true;

	// Use this for initialization
	void Start () {
        StartingColor = BackroundImage.color;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (triggerFadeIn)
            StartCoroutine(Fade());

    }

    IEnumerator Fade()
    {
        if (BackroundImage.color.a < 255f)
        {
            Debug.Log("derp");
            BackroundImage.color = new Color(BackroundImage.color.r, BackroundImage.color.g, BackroundImage.color.b, BackroundImage.color.a + fadeSpeed);
            GameOverText.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, GameOverText.color.a + fadeSpeed);
        } else
        {
            triggerFadeIn = false;
            yield return 0;
        }
        
    }
}
