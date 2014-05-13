using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace myEp3
{
    class Audio
    {
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        public Cue trackCue;
        public string cueName;
        public bool playStat;
        public bool isPlaying;
        

        public Audio(string ae, string wb, string sb, string cn)
        {
            this.audioEngine = new AudioEngine(ae);
            this.waveBank = new WaveBank(audioEngine, wb);
            this.soundBank = new SoundBank(audioEngine, sb);
            this.cueName = cn;
            playStat = false;
        }

        public void PlayContinues()
        {
            if (!playStat)
            {
                this.trackCue = soundBank.GetCue(this.cueName);
                this.trackCue.Play();
                playStat = true;
            }

        }

        public void PauseContinues()
        {
            if (playStat)
            {
                this.trackCue.Pause();
                playStat = false;
            }
        }

        public void Play()
        {
                this.trackCue = soundBank.GetCue(this.cueName);
                this.trackCue.Play();

        }

        public void Pause()
        {
                
                this.trackCue.Pause();
        }

        public void Resume()
        {
            this.trackCue.Resume();
        }

        public void Stop()
        {
            this.trackCue.Stop(AudioStopOptions.Immediate);
        }




    }
}
