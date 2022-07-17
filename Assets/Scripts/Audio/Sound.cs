using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Sound
{
    public string Name;
    
    public AudioClip Clip;
    
    [Range(0, 1)] public float Volume;
    [Range(.1f, 3f)] public float Pitch;

    public Sound(string audioPath, string name, float volume = 1, float pitch = 1)
    {
        Debug.Log(audioPath);
        
        // Modified version of https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequestMultimedia.GetAudioClip.html
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioPath, AudioType.OGGVORBIS))
        {
            UnityWebRequestAsyncOperation newWeb= www.SendWebRequest();
            
            while (!www.isDone)
            {
                
            }
            
            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Clip = DownloadHandlerAudioClip.GetContent(www);
            }
        }
        
        Debug.Log(Clip);
        
        Name = name;
        Volume = volume;
        Pitch = pitch;
    }
}
