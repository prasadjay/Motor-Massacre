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
    class Vehicle
    {
        public Model model { get; protected set; }
        public Matrix world;
        Matrix shipLocation;
        Vector3 location;
        Matrix worldRotation;
        Quaternion shipRotation;
        Vector3 movement;
        public Vector3 scale;
        public string carName;

        public Vehicle(Model m, Vector3 loc, Vector3 scale, string name)
        {
            model = m;
            world = Matrix.CreateTranslation(loc);
            shipLocation = Matrix.CreateTranslation(loc);
            worldRotation = shipLocation;
            world = Matrix.CreateScale(0.1f) * shipLocation;
            location = loc;
            shipRotation = Quaternion.Identity;
            this.scale = scale;

        }

        //public void update()
        //{
        //    KeyboardState ks = Keyboard.GetState();
        //    float x = Matrix.CreateFromQuaternion(shipRotation).Forward.X;
        //    float y = Matrix.CreateFromQuaternion(shipRotation).Forward.Y;
        //    float z = Matrix.CreateFromQuaternion(shipRotation).Forward.Z;
        //    movement = new Vector3(x, y, z);

        //    if (ks.IsKeyDown(Keys.W))
        //    {

        //        shipLocation *= Matrix.CreateTranslation(movement * 0.15f);
        //        //worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4/45);
        //    }
        //    if (ks.IsKeyDown(Keys.S))
        //    {

        //        shipLocation *= Matrix.CreateTranslation(movement * -0.15f);
        //        // worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / -45);
        //    }

        //    if (ks.IsKeyDown(Keys.A))
        //    {
        //        worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / 45);
        //        shipRotation = Quaternion.CreateFromRotationMatrix(worldRotation);
        //    }
        //    if (ks.IsKeyDown(Keys.D))
        //    {
        //        worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / -45);
        //        shipRotation = Quaternion.CreateFromRotationMatrix(worldRotation);
        //    }         

        //    world = worldRotation * shipLocation * shipLocation;

        //}

        public void update(int speed)
        {
            float val = (float)(1.0 / speed);

            KeyboardState ks = Keyboard.GetState();
            float x = Matrix.CreateFromQuaternion(shipRotation).Forward.X;
            float y = Matrix.CreateFromQuaternion(shipRotation).Forward.Y;
            float z = Matrix.CreateFromQuaternion(shipRotation).Forward.Z;
            movement = new Vector3(x, y, z);

            if (ks.IsKeyDown(Keys.W))
            {
                // shipLocation *= Matrix.CreateTranslation(0, 0, val);
                // shipLocation *= Matrix.CreateTranslation(movement * val);

            }
            if (ks.IsKeyDown(Keys.S))
            {
                // shipLocation *= Matrix.CreateTranslation(0, 0, val);
                //shipLocation *= Matrix.CreateTranslation(movement * -val);

            }

            if (ks.IsKeyDown(Keys.A))
            {
                worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / 45);
                shipRotation = Quaternion.CreateFromRotationMatrix(worldRotation);
            }
            if (ks.IsKeyDown(Keys.D))
            {
                worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / -45);
                shipRotation = Quaternion.CreateFromRotationMatrix(worldRotation);
            }

            if ((speed != 100 && speed != -100))
            {
                shipLocation *= Matrix.CreateTranslation(movement * -val);
            }
            world = worldRotation * shipLocation * shipLocation;

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
                    //b.PreferPerPixelLighting = true;  // Activate this and whole model goes WHITE color!
                    b.Projection = camera.projection;
                    b.View = camera.view;
                    b.World = getWorld() * mesh.ParentBone.Transform;
                    b.LightingEnabled = true; // turn on the lighting subsystem.

                    b.DirectionalLight0.DiffuseColor = new Vector3(1f, 1f, 1f); // color of light
                    b.DirectionalLight0.Direction = new Vector3(1, 1, 1);  // coming along the x-axis
                    b.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1); // with white highlights.. Change RGB values to change highlight color for a model.

                    //Lasitha - We can add up to 3 Directional lights. Use that lighting as you want in other classes you make .. like b.DirectionalLight1.Diffusecolor = blah blah

                    b.AmbientLightColor = new Vector3(10f, 10f, 10f);
                    b.DiffuseColor = new Vector3(10f, 10f, 10f);
                    b.EmissiveColor = new Vector3(1, 1, 1);
                    b.SpecularColor = Vector3.One;

                }

                mesh.Draw();
            }

        }

        public void fall()
        {
            shipLocation *= Matrix.CreateTranslation(0, -0.01f, 0);
        }

        public virtual Vector3 getMovement()
        {
            return movement;
        }


        public virtual Matrix getWorld()
        {
            return Matrix.CreateScale(scale) * this.world;
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
        public virtual Quaternion getQuaRot()
        {
            return shipRotation;
        }

        public void rotateZ90()
        {
            worldRotation *= Matrix.CreateRotationZ(-MathHelper.PiOver2);
        }

        public void rotateX90()
        {
            worldRotation *= Matrix.CreateRotationX(-MathHelper.PiOver2);
        }
        public void rotateY90()
        {
            worldRotation *= Matrix.CreateRotationY(-MathHelper.PiOver2);
        }
    }
}


/*
public Model model { get; protected set; }
        public Matrix world = Matrix.Identity;
        public Matrix worldTranslation = Matrix.Identity;
        public Matrix worldRotation = Matrix.Identity;
        public float scaleX;
        public float scaleY;
        public float scaleZ;

        public Vehicle(Model m, Vector3 position, float sX, float sY, float sZ)
        {
            model = m;
            worldTranslation = Matrix.CreateTranslation(position);
            this.scaleX = sX;
            this.scaleY = sY;
            this.scaleZ = sZ;
        }

        public void update()
        {
            
            world = worldTranslation*worldRotation;
            
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
                    //b.PreferPerPixelLighting = true;  // Activate this and whole model goes WHITE color!
                    b.Projection = camera.projection;
                    b.View = camera.view;
                    b.World = getWorld() * mesh.ParentBone.Transform;
                    b.LightingEnabled = true; // turn on the lighting subsystem.

                    //b.DirectionalLight0.DiffuseColor = new Vector3(1f, 1f, 1f); // color of light
                    //b.DirectionalLight0.Direction = new Vector3(0, -5, 5);  // coming along the x-axis
                    //b.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1); // with white highlights.. Change RGB values to change highlight color for a model.

                    //Lasitha - We can add up to 3 Directional lights. Use that lighting as you want in other classes you make .. like b.DirectionalLight1.Diffusecolor = blah blah

                    //b.AmbientLightColor = new Vector3(10f, 10f, 10f);
                    //b.DiffuseColor = new Vector3(10f, 10f, 10f);
                    //b.EmissiveColor = new Vector3(1, 0, 0);
                    //b.SpecularColor = Vector3.One;

                }

                mesh.Draw();
            }

        }

        public void changeModelScale(float valueX, float valueY, float valueZ)
        {
            this.scaleX = valueX;
            this.scaleY = valueY;
            this.scaleZ = valueZ;
        }

        public Matrix getWorld()
        {
            return Matrix.CreateScale(new Vector3(scaleX, scaleY, scaleZ)) * this.world;
        }

        public Vector3 getPosition()
        {
            Vector3 pos = Matrix.Invert(this.getWorld()).Translation;
            return pos;
        }

        public void setModel(Model m)
        { 
            model = m;
        }
*/