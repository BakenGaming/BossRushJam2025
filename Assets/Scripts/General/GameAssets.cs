using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class GameAssets : MonoBehaviour
{
    #region Setup
    private static GameAssets _i;

    public static GameAssets i
    { 
      get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }  
    }

    private void Awake() 
    {
        _i = this;    
    }
    #endregion
    #region Object Assets
    [Header("Messages and Popups")]
    public Transform pfSysMessage;
    public Transform pfDamagePopup;
    public Transform pfTextPopup;
    [Header("Player Specific")]
    public GameObject pfPlayerObject;
    public GameObject pfPlayerDamageParticles;
    [Header("Universal")]
    
    [Header("Boss Specific")]
    public GameObject pfBossObject;
    public GameObject pfLootObject;
    public GameObject pfBerryBomb;
    public GameObject pfBerry;
    public GameObject pfBerrySplash;

    [Header("UI")]
    public GameObject pfWeaponOptionRoulette;
    #endregion

    #region Sound Assets
    
    [Header("Sound")]
    public SoundAudioClip[] audioClipArray;
    public MusicAudioClip[] musicTrackArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioMixerGroup mixer;
        public SoundManager.Sound sound;
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume;
    }

    [System.Serializable]
    public class MusicAudioClip
    {
        public AudioMixerGroup mixer;
        public SoundManager.Music music;
        public AudioClip audioClip;
        [Range(0f, 1F)]
        public float volume;
    }
    #endregion
}
