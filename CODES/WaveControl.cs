using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveControl : MonoBehaviour
{

    public Sprite play;
    public Sprite pause;
    public Sprite ff;
    public Sprite ffclicked;

    public Image ppImage;
    public Image ffImage;

    private bool paused = false;
    private bool ffed = false;


    public void Pause()
	{
        paused = paused ? false : true;
		if(Time.timeScale != 0f)
		{
            AudioListener.pause = true;
			Time.timeScale = 0f;
		}
		else
		{
            AudioListener.pause = false;
            Time.timeScale = ffed ? 2f : 1f;
		}

        ChangeImage();
	}

    public void FastForward()
    {
        if(ffed)
        {
            ffed = false;
            ffImage.sprite = ff;
        }
        else
        {
            ffed = true;
            ffImage.sprite = ffclicked;
        }
        
        if(Time.timeScale != 2f)
		{
            if(!paused)
            {
			    Time.timeScale = 2f;
            }

		}
		else
		{
            if(!paused)
            {
			    Time.timeScale = 1f;
            }
		}
    }
    
    public void ChangeImage()
    {
        if(ppImage.sprite == play)
        {
            ppImage.sprite = pause;
        }
        else
        {
            ppImage.sprite = play;
        }
    }

    public void PauseMenuOpen()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        else
        {

            AudioListener.pause = true;
            Time.timeScale = 0f;
        }

    }

    public void PauseMenuClose()
    {
        if(paused)
        {
            
        }
        else
        {
            AudioListener.pause = false;
            Time.timeScale = ffed ? 2f : 1f;
        }

    }



    public void Reset()
    {
        ppImage.sprite = play;

        //Time.timeScale = 1f;
    }
}
