using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Music
    [SerializeField] AudioClip _menuMusic;
    [SerializeField] AudioClip _gameMusic;
    [SerializeField] AudioClip _townMusic;
    [SerializeField] AudioClip _bossArea;
    [SerializeField] AudioClip _bossFight;

    // Sound Effects
    [SerializeField] AudioClip _sceneSwitchSwoosh;
    [SerializeField] AudioClip _fireball;
    [SerializeField] AudioClip _iceNova;
    [SerializeField] AudioClip _meleeCombatSFX;
    [SerializeField] AudioClip _hitReaction;
    [SerializeField] AudioClip _uiSoundEffect;
    [SerializeField] AudioClip _inventorySFX;
    [SerializeField] AudioClip _lootPickup;
    [SerializeField] AudioClip _healthRestored;
    [SerializeField] AudioClip _manaRestored;
    [SerializeField] AudioClip _dialogueSFX;

    [Space(10)]
    [SerializeField] AudioSource _musicChannel;
    [SerializeField] List<AudioSource> _sfxChannels;

    [SerializeField] private AudioSource _voiceAudioChannel;

    int currentSFXChannel = 0;
    int highestSFXChannel = 0;

    public static AudioManager Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (_musicChannel == null) Debug.LogError("AudioManager: Music Channel is null");
        foreach (AudioSource sfxChannel in _sfxChannels)
        {
            if (sfxChannel == null) Debug.LogError("Audio Manager: One of the SFX Channels is null");
        }

        highestSFXChannel = _sfxChannels.Count - 1;
    }

    #region Music
    void PlayMusic(AudioClip music)
    {
        if (music == null)
        {
            Debug.LogWarning("Attempted to play a null music clip.  Not playing music");
            return;
        }
        // This conditional will only allow swapping of the music if the music is changing
        //  - This lets multiple areas with the same music feel contiguous
        if (_musicChannel.clip != music)
        {
            _musicChannel.Stop();
            _musicChannel.clip = music;
            _musicChannel.Play();
        }
    }

    public void PlayMenuMusic()
    {
        PlayMusic(_menuMusic);
    }

    public void PlayTownMusic()
    {
        PlayMusic(_townMusic);
    }

    public void PlayGameMusic()
    {
        PlayMusic(_gameMusic);
    }

    public void PlayBossAreaMusic()
    {
        PlayMusic(_bossArea);
    }

    public void PlayBossFightMusic()
    {
        PlayMusic(_bossFight);
    }

    public void StopMusic()
    {
        if(_musicChannel.isPlaying) _musicChannel.Stop();
    }
    #endregion

    #region Sound Effects
    void PlaySoundEffect(AudioClip soundEffect)
    {
        if (soundEffect == null)
        {
            Debug.LogWarning("Attempted to play a null sfx clip.  Not playing sfx");
            return;
        }

        NextSFXChannel();
        if (!_sfxChannels[currentSFXChannel].isPlaying)
        {
            _sfxChannels[currentSFXChannel].Stop();
            _sfxChannels[currentSFXChannel].clip = soundEffect;
            _sfxChannels[currentSFXChannel].Play();
        }
    }

    public void PlaySceneSwitchSwooshSFX()
    {
        PlaySoundEffect(_sceneSwitchSwoosh);
    }
    public void PlayFireballSFX()
    {
        PlaySoundEffect(_fireball);
    }

    public void PlayIceNovaSFX()
    {
        PlaySoundEffect(_iceNova);
    }

    public void PlayHitReactionSFX()
    {
        PlaySoundEffect(_hitReaction);
    }

    public void PlayMeleeCombatSFX()
    {
        PlaySoundEffect(_meleeCombatSFX);
    }

    public void PlayUIButtonSFX()
    {
        PlaySoundEffect(_uiSoundEffect);
    }

    public void PlayLootPickupSFX()
    {
        PlaySoundEffect(_lootPickup);
    }

    public void PlayInventorySFX()
    {
        PlaySoundEffect(_inventorySFX);
    }

    public void PlayDialogueSFX()
    {
        PlaySoundEffect(_dialogueSFX);
    }

    public void PlayHealthRestoredSFX()
    {
        PlaySoundEffect(_healthRestored);
    }

    public void PlayManaRestoredSFX()
    {
        PlaySoundEffect(_manaRestored);
    }

    // This cycles the indices of the sfx channel list and makes "currentSFXChannel" appropriate throughout the class
    // - This is called by PlayMusic() and PlaySoundEffect() before stopping the sound/music, replacing the clip, and playing the new clip
    void NextSFXChannel()
    {
        currentSFXChannel++;
        if (currentSFXChannel > highestSFXChannel)
            currentSFXChannel = 0;

    }
    #endregion

    #region Voice Acting
    public void PlayVoiceLine(AudioClip voiceLine)
    {
        if (voiceLine == null) { return; }

        if (_voiceAudioChannel.clip != voiceLine)
        {
            _voiceAudioChannel.Stop();
            _voiceAudioChannel.clip = voiceLine;
            _voiceAudioChannel.Play();
        }
    }
    #endregion
}
