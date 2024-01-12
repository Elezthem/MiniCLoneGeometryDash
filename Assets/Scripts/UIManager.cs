using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] Button playButton;
    [SerializeField] Text maintext;
    [SerializeField] Image bg;
    [SerializeField] AudioBehaviour ms;
    [SerializeField] AudioBehaviour stereoMadness;
    [SerializeField] Text Attempt;
    [SerializeField] Canvas Canvas;
    [SerializeField] Button CreateBtn;
    [SerializeField] Image pause;
    [SerializeField] Button setting;

    public void PlayGame()
    {
        if(!Global.Playmode)
        {
            Global.Playmode = true;
            playButton.gameObject.SetActive(false);
            maintext.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);
            ms.gameObject.SetActive(false);
            stereoMadness.gameObject.SetActive(true);
            Attempt.gameObject.SetActive(true);
            Canvas.gameObject.SetActive(true);
            CreateBtn.gameObject.SetActive(false);
            pause.gameObject.SetActive(true);
            setting.gameObject.SetActive(false);
        }
    }
}
