using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public Image backgroundImage;

	// Use this for initialization
	void Start () {
		
	}

    bool isFading = false;
    float t = 0;
	// Update is called once per frame
	void Update () {

        if (isFading) {

            t += Time.deltaTime / 4f;

            if (t >= 1f)
            {
                t = 1f;
                isFading = false;
            }

            var color = backgroundImage.color;
            color.a = Mathf.Lerp(0.04f, 1f, t);
            backgroundImage.color = color;
        }
	}

    public void FadeOut()
    {
        isFading = true;
    }
}
