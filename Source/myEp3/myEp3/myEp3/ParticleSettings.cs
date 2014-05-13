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


namespace myEp3
{
    class ParticleSettings
    {
        //size of particle
        public int maxSize = 2;
    }

    class ParticleExplosionSettings
    {
        //Life of Particle
        public int minLife = 1000;
        public int maxLife = 2000;

        //particles per round
        public int minPerRound = 100;
        public int maxPerRound = 600;

        //Round Time
        public int minRoundTime = 16;
        public int maxRoundTime = 50;

        //Number of Particles
        public int minParticles = 20;
        public int maxParticles = 30;
    }

}
