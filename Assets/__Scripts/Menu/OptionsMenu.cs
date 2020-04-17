using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private TextMeshProUGUI volumeText;

    public void SetVolume(float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
        volumeText.text = $"Volume: {((volume + 80) / 80)*100:0}%"; // convert -80 to 0 dB scale to a percentage for UI
    }
}
