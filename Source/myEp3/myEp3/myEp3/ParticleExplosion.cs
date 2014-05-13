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
    class ParticleExplosion
    {

        //particle arras and vertex buffer

        VertexPositionTexture[] verts;
        Vector3[] vertexDirectionArray;
        Color[] vertexColorArray;
        VertexBuffer particleVertexBuffer;

        //position
        Vector3 position;

        //Life
        int lifeLeft;

        //Round and Particle Counts
        int numParticlesPerRound;
        int maxParticles;
        static Random rnd = new Random();
        int roundTime;
        int timeSinceLastRound;

        //Vertex and graphic info
        GraphicsDevice graphicsDevice;

        //Settings
        ParticleSettings particleSettings;

        //Effects
        Effect particleEffect;

        //Textures
        Texture2D particleColorsTexture;

        //Array indices
        int endOfLiveParticlesIndex = 0;
        int endOfDeadParticlesIndex = 0;

        public ParticleExplosion(GraphicsDevice gs, Vector3 pos, int life, int time, int perRound, int maxParticles, Texture2D tex, ParticleSettings ps, Effect ef)
        {
            this.position = pos;
            this.lifeLeft = life;
            this.numParticlesPerRound = perRound;
            this.maxParticles = maxParticles;
            this.roundTime = time;
            this.graphicsDevice = gs;
            this.particleEffect = ef;
            this.particleColorsTexture = tex;
            this.particleSettings = ps;

            InitializeParticleVerts(); 
        }

        private void InitializeParticleVerts()
        {
            //instantiate all particle arrays
            verts = new VertexPositionTexture[maxParticles * 4];
            vertexDirectionArray = new Vector3[maxParticles];
            vertexColorArray = new Color[maxParticles];

            //get color data from colors texture
            Color[] colors = new Color[particleColorsTexture.Width*particleColorsTexture.Height];
            particleColorsTexture.GetData(colors);

            //loop until max particles
            for (int i = 0; i < maxParticles; ++i)
            {
                float size = (float)rnd.NextDouble() * particleSettings.maxSize;

                //set position direction and size of particle
                verts[i * 4] = new VertexPositionTexture(position, new Vector2(0, 0));
                verts[(i * 4) + 1] = new VertexPositionTexture(new Vector3(position.X, position.Y + size, position.Z), new Vector2(0,1));
                verts[(i * 4) + 2] = new VertexPositionTexture(new Vector3(position.X + size, position.Y, position.Z), new Vector2(1, 0));
                verts[(i * 4) + 3] = new VertexPositionTexture(new Vector3(position.X+size, position.Y+size, position.Z), new Vector2(1, 1));

                //create a random velocity or direction
                Vector3 direction = new Vector3((float)rnd.NextDouble() * 2 - 1, (float)rnd.NextDouble() * 2 - 1, (float)rnd.NextDouble() * 2 - 1);
                direction.Normalize();

                //multiply by next double to make sure that all particles move a t random speeds
                direction *= (float)rnd.NextDouble();

                //set direction of particle 
                vertexDirectionArray[i] = direction;

                //SetDataOptions color ArgumentOutOfRangeException particle by getting as Random color from the Texture
                vertexColorArray[i] = colors[(rnd.Next(0, particleColorsTexture.Height)*particleColorsTexture.Width)+rnd.Next(0, particleColorsTexture.Width)];
            }

            //instantiate vertex buffer
            particleVertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);

        }



        public bool isDead()
        {
            if (endOfDeadParticlesIndex == maxParticles)
                return true;
            else
                return false;
        }

        public void UpdateParticles(GameTime gameTime)
        {
            //decrement life untill its gone
            if (lifeLeft > 0)
                lifeLeft -= gameTime.ElapsedGameTime.Milliseconds;

            //time for new round
            timeSinceLastRound += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastRound > roundTime)
            {
                //new round add and remove particlews
                timeSinceLastRound -= roundTime;

                //incrememnt end of live particles indexeach round untill end of list is reached
                if (endOfLiveParticlesIndex < maxParticles)
                {
                    endOfLiveParticlesIndex += numParticlesPerRound;
                    if (endOfLiveParticlesIndex > maxParticles)
                        endOfLiveParticlesIndex = maxParticles;
                }

                if (lifeLeft <= 0)
                {
                    //increment end of dead particles index each round until end od list is reached
                    if (endOfDeadParticlesIndex < maxParticles)
                    {
                        endOfDeadParticlesIndex += numParticlesPerRound;
                        if (endOfDeadParticlesIndex > maxParticles)
                            endOfDeadParticlesIndex = maxParticles;
                    }
                }
            }


            //update positions of all live particles

            for (int i = endOfDeadParticlesIndex; i < endOfLiveParticlesIndex; ++i)
            {
                verts[i * 4].Position += vertexDirectionArray[i];
                verts[(i * 4) + 1].Position += vertexDirectionArray[i];
                verts[(i * 4) + 2].Position += vertexDirectionArray[i];
                verts[(i * 4) + 3].Position += vertexDirectionArray[i];
            }
        }

        public void DrawParticles(Camera camera)
        {
            graphicsDevice.SetVertexBuffer(particleVertexBuffer);

            if (endOfLiveParticlesIndex - endOfDeadParticlesIndex > 0)
            {
                for (int i = endOfDeadParticlesIndex; i < endOfLiveParticlesIndex; i++)
                {
                    particleEffect.Parameters["WorldViewProjection"].SetValue(camera.view*camera.projection);
                    particleEffect.Parameters["particleColor"].SetValue(vertexColorArray[i].ToVector4());

                    //draw particles

                    foreach (EffectPass pass in particleEffect.CurrentTechnique.Passes)
                    {
                        pass.Apply();

                        graphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, verts, i * 4 , 2);
                    }
                }
            }
        }



    }
}
