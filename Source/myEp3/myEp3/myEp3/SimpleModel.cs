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
    class SimpleModel
    {
        public Model model { get; protected set; }
        public Matrix world = Matrix.Identity;
        public Matrix worldTranslation = Matrix.Identity;
        public Matrix worldRotation = Matrix.Identity;
        public Matrix scale;
        public float xScale;
        public float yScale;
        public float zScale;

        public SimpleModel(Model m, Vector3 position, float x, float y, float z)
        {
            model = m;
            worldTranslation = Matrix.CreateTranslation(position);
            this.xScale = x;
            this.yScale = y;
            this.zScale = z;

            world = Matrix.CreateScale(new Vector3(x, y, z)) * this.world;
        }

        public void update()
        {

            //no keyboard updates as there are no movements in simpleModel Class.. Implement keyboard for Vehicle class

            world = worldTranslation * worldRotation;

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
                    b.DirectionalLight0.Direction = new Vector3(-1, 0, 0);  // coming along the x-axis
                    b.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1); // with white highlights.. Change RGB values to change highlight color for a model.

                    //Lasitha - We can add up to 3 Directional lights. Use that lighting as you want in other classes you make .. like b.DirectionalLight1.Diffusecolor = blah blah

                    b.AmbientLightColor = new Vector3(1f, 1f, 1f);
                    b.DiffuseColor = new Vector3(1f, 1f, 1f);
                    b.EmissiveColor = new Vector3(1, 1, 1);
                    //b.SpecularColor = Vector3.One;

                }

                mesh.Draw();
            }

        }

        public void changeModelScale(float x, float y, float z
)
        {
            this.scale = Matrix.CreateScale(new Vector3(x, y, z)) * this.world; ;
        }

        public Matrix getWorld()
        {
            return Matrix.CreateScale(new Vector3(xScale, yScale, zScale)) * this.world;
        }

        public Vector3 getPosition()
        {
            Vector3 pos = Matrix.Invert(this.getWorld()).Translation;
            return pos;
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

        //prasad methods

        public void rotateYplus90()
        {
            worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver2);
        }

        public void rotateYminus90()
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

        public SimpleModel(Model m, Vector3 position, float sX, float sY, float sZ)
        {
            model = m;
            worldTranslation = Matrix.CreateTranslation(position);
            this.scaleX = sX;
            this.scaleY = sY;
            this.scaleZ = sZ;
        }

        public void update()
        {
            
            //no keyboard updates as there are no movements in simpleModel Class.. Implement keyboard for Vehicle class

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


*/