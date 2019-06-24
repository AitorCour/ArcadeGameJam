using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
}
