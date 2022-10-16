using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", 1);
            load();
        }
    }

    public void changeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        save();
         
    }

    private void load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("music");
    }

    private void save()
    {
        PlayerPrefs.SetFloat("music", volumeSlider.value);
    }
}
