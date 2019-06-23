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
    public Button but;
    public void ChangeImageStart()
    {
        if (but.image.sprite == OnSpriteStart)
        {
            but.image.sprite = OffSpriteStart;
        }
        else
        {
            but.image.sprite = OnSpriteStart;
        }
    }
    public void ChangeImageLoad()
    {
        if (but.image.sprite == OnSpriteLoad)
        {
            but.image.sprite = OffSpriteLoad;
        }
        else
        {
            but.image.sprite = OnSpriteLoad;
        }
    }
    public void ChangeImageExit()
    {
        if (but.image.sprite == OnSpriteExit)
        {
            but.image.sprite = OffSpriteExit;
        }
        else
        {
            but.image.sprite = OnSpriteExit;
        }
    }
}
