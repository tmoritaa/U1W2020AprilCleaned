using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Zenject;

namespace Outputs.Audio {
  public class GameSeAudioPlayer : MonoBehaviour, ISeAudioPlayer, IInitializable {
    [SerializeField] private List<AudioEntry> audioEntries;

    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

    public void Initialize() {
      foreach (var audioEntry in this.audioEntries) {
        this.audioSources.Add(audioEntry.Id, audioEntry.AudioSource);
      }
    }

    public void Play(string id) {
      this.audioSources[id].Play();
    }

    [Serializable]
    private struct AudioEntry {
      [SerializeField] private string id;
      public string Id => this.id;

      [SerializeField] private AudioSource audioSource;
      public AudioSource AudioSource => this.audioSource;
    }
  }
}