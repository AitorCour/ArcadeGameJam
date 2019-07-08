using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Settings : MonoBehaviour
{
    public Sprite OffSpriteStart;
    public Sprite OnSpriteStart;
    public Sprite OffSpriteLoad;
    public Sprite OnSpriteLoad;
    public Sprite OffSpriteExit;
    public Sprite OnSpriteExit;
    public Button butS;
    public Button butL;
    public Button butE;

    public Image black;
    public Animator animator;

    public void ChangeImageStart()
    {
        if (butS.image.sprite == OnSpriteStart)
        {
            butS.image.sprite = OffSpriteStart;
        }
        else
        {
            butS.image.sprite = OnSpriteStart;
        }
    }
    public void ChangeImageLoad()
    {
        if (butL.image.sprite == OnSpriteLoad)
        {
            butL.image.sprite = OffSpriteLoad;
        }
        else
        {
            butL.image.sprite = OnSpriteLoad;
        }
    }
    public void ChangeImageExit()
    {
        if (butE.image.sprite == OnSpriteExit)
        {
            butE.image.sprite = OffSpriteExit;
        }
        else
        {
            butE.image.sprite = OnSpriteExit;
        }
    }
    public void ChangeScene(int scene)
    {
        StartCoroutine(FadeButton(scene));
        Debug.Log("change_1");
    }
    IEnumerator FadeButton(int scene)
    {
        Debug.Log("change");

        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        //settings.SaveVolume();

        SceneManager.LoadScene(scene);
    }
}
