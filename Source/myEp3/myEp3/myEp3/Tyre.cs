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
    class Tyre
    {
        public Model model { get; protected set; }
        public Matrix world;// = Matrix.Identity;
        Matrix worldTranslation;
        Matrix worldRotation;
        Quaternion rotation;
        Vector3 location;

        public Tyre(Model m, Vector3 loc, float sclare)
        {
            model = m;
            world = Matrix.CreateTranslation(loc);
            worldTranslation = Matrix.CreateTranslation(loc);
            worldRotation = Matrix.Identity;

        }

        static int speed = 100;
        float runTime;
        int turn = 0;
        float angle = 0;
        double elapsedTime = 0f;
        public void update_backTires(GameTime gametime)
        {
            KeyboardState ks = Keyboard.GetState();
            runTime = gametime.ElapsedGameTime.Milliseconds;

            //movements
            float val = (float)(1.0 / speed);
            if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.S))//forward and backwards
            {
                //worldTranslation *= Matrix.CreateTranslation(0, 0, val);
            }


            // forwards
            if (ks.IsKeyDown(Keys.S))
            {
                if (speed >= 5 && speed <= 100)
                    worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / --speed);

                else if (speed == -100)
                    speed = 100;

                else if (speed <= -5)
                    worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / -(--speed));

                else if (speed > 0)
                    worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / speed);
                else if (speed < 0)
                    worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / speed);
            }

           //backwards
            else if (ks.IsKeyDown(Keys.W))
            {
                if (speed < 100 && speed >= 0)
                    worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / (++speed));

                else if (speed == 100)
                    speed = -100;

                else if (speed >= -100 && speed < -5)
                    worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / (++speed));

                else if (speed > 0)
                    worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / -speed);
                else if (speed < 0)
                    worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / speed);
            }

            else if (!ks.IsKeyDown(Keys.S) && !ks.IsKeyDown(Keys.W))
            {
                if (speed < 100 && speed > -100)
                    worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / speed);

                if (speed > 0 && speed < 100) //slowing down accleration
                {
                    slowSpeed(gametime);

                }
                else if (speed < 0 && speed > -100)
                {
                    slowSpeed(gametime);
                }
            }

            if (speed == 100 || speed == -100) { }

            else
            {
                //worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / speed);
                //worldTranslation *= Matrix.CreateTranslation(0, 0, val);
            }

            if (ks.IsKeyDown(Keys.A))
            {
                //this.worldTranslation = Matrix.Transform(worldTranslation,rotation);
            }
            if (ks.IsKeyDown(Keys.D))
            {
                //this.worldTranslation = Matrix.Transform(worldTranslation, rotation);
            }
            this.worldTranslation = Matrix.Transform(Matrix.CreateTranslation(location), rotation);
            //worldTranslation = Matrix.CreateTranslation(location);
            world = worldRotation * worldTranslation;

        }



        private void slowSpeed(GameTime gametime) //used for slowing down movement
        {

            elapsedTime += gametime.ElapsedGameTime.TotalMilliseconds;
            if (speed <= -5 && speed > -10)
            {
                if (elapsedTime >= 500)
                {
                    elapsedTime = 0;
                    speed--;
                }
            }
            else if (speed > 3 && speed < 10)
            {
                if (elapsedTime >= 500)
                {
                    elapsedTime = 0;
                    speed++;
                }
            }
            else if (speed < 0)
            {
                if (elapsedTime >= 100)
                {
                    elapsedTime = 0;
                    speed--;
                }
            }
            else if (speed > 0)
            {
                if (elapsedTime >= 100)
                {
                    elapsedTime = 0;
                    speed++;
                }
            }
        }

        public void update_frontTires(GameTime gametime)//front tire actions
        {
            KeyboardState ks = Keyboard.GetState();

            this.update_backTires(gametime);

            //turning
            if (ks.IsKeyDown(Keys.A) && turn < 75)//left
            {
                worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / speed);
                angle += MathHelper.PiOver4 / speed;
                turn++;
            }
            else if (ks.IsKeyDown(Keys.D) && turn > -75)//right
            {
                worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / -speed);
                angle -= MathHelper.PiOver4 / speed;
                turn--;
            }

            if (!ks.IsKeyDown(Keys.D) && !ks.IsKeyDown(Keys.A)) //reset the tire turn 
            {
                elapsedTime += gametime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTime >= 25)
                {
                    elapsedTime = 0;
                    if (turn != 0)
                    {
                        if (turn > 0)
                        {
                            worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / -speed);

                            if (speed < 100 && speed > -100)
                            {
                                worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / speed);

                            }

                            if (speed > 0 && speed < 100) //slowing down accleration
                            {
                                slowSpeed(gametime);

                            }
                            else if (speed < 0 && speed > -100)
                            {
                                slowSpeed(gametime);
                            }
                            angle -= MathHelper.PiOver4 / speed;
                            turn--;

                            if (turn == 0) angle = 0;
                        }
                        else if (turn < 0)
                        {
                            worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / speed);
                            turn++;
                            angle += MathHelper.PiOver4 / speed;
                            if (turn == 0) angle = 0;
                        }
                    }
                }

            }


            world = worldRotation * worldTranslation * worldTranslation;
        }

        public void setTires(Vector3 move, Vector3 offset, GameTime gametime, Quaternion rotation)
        {
            this.rotation = rotation;
            location = move + offset;
            //worldTranslation *= Matrix.CreateTranslation(location);
             world *= Matrix.CreateTranslation(location);
            this.update_backTires(gametime);


        }

        public void fall()
        {
            worldTranslation = Matrix.CreateTranslation(0, -.01f, 0);
        }

        public void Draw(Camera camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect b in mesh.Effects)
                {
                    b.EnableDefaultLighting();
                    b.Projection = camera.projection;
                    b.View = camera.view;
                    b.World = getMyWorld() * mesh.ParentBone.Transform;

                }

                mesh.Draw();
            }

        }

        public virtual Matrix getMyWorld()
        {
            return Matrix.CreateScale(0.33f) * world;
        }

        //return the current location of object
        public virtual Vector3 getWorldLoc()
        {
            return world.Translation;
        }
        public virtual Matrix getWorldRot()
        {
            return worldRotation;
        }
        public int getSpeed()
        {
            return speed;
        }
        public double getTimer()
        {
            return elapsedTime;
        }
        public float getTurn()
        {
            return angle;
        }

    }
}
