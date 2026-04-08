using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail);
    }
    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume =1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position,volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume =1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
