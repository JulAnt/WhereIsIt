using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    void Start(){
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        
        while (t > 0f){
            t -= Time.deltaTime;
            img.color = new Color (0f, 0f, 0f, t);
            yield return 0;

        }
    }
}
