using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace myEp3
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {


        public Matrix view { get;  set; }
        public Matrix projection { get; protected set; }

        float speed = 2;
        float speed2 = 0.5f;

        MouseState prevMouseState;

        //camera vectors

        public Vector3 cameraPosition { get; protected set; }
        Vector3 cameraDirection;
        Vector3 cameraUp;


        public Camera(Game game, Vector3 position, Vector3 target, Vector3 up)
            : base(game)
        {
            // TODO: Construct any child components here

            cameraPosition = position;
            cameraDirection = target - position;
            cameraDirection.Normalize();
            cameraUp = up;
            CreateLookAt();
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 2, (float)Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, 1, 3000);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);
            prevMouseState = Mouse.GetState();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            //controllers
            KeyboardState ks = Keyboard.GetState();

            //follow car

            //if (ks.IsKeyDown(Keys.W))
            //    cameraPosition += cameraDirection * speed2;
            //if (ks.IsKeyDown(Keys.S))
            //    cameraPosition -= cameraDirection * speed2;
            //if (ks.IsKeyDown(Keys.A))
            //{
            //    //worldRotation *=  Matrix.CreateRotationY(MathHelper.PiOver4 / 60);
            //    cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 25)));
            //    cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed2;
            //}
            //if (ks.IsKeyDown(Keys.D))
            //{
            //    //worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / -60);
            //    cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed2;
            //}

            //Move forward

            if (ks.IsKeyDown(Keys.Up))
                cameraPosition += cameraDirection * speed;

            //Move backwards

            if (ks.IsKeyDown(Keys.Down))
                cameraPosition -= cameraDirection * speed;

            //Move left

            if (ks.IsKeyDown(Keys.Left))
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;

            //Move right

            if (ks.IsKeyDown(Keys.Right))
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed;

            //YAW - around Y axis
            if (ks.IsKeyDown(Keys.NumPad4) || ks.IsKeyDown(Keys.D))
            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 45) ));

            if (ks.IsKeyDown(Keys.NumPad6) || ks.IsKeyDown(Keys.A))
            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, (MathHelper.PiOver4 / 45)));

            ////ROLL - around Z axis

            if (ks.IsKeyDown(Keys.Add))
                cameraUp = Vector3.Transform(cameraUp, Matrix.CreateFromAxisAngle(cameraDirection, MathHelper.PiOver4 / 45));

            if (ks.IsKeyDown(Keys.Subtract))
                cameraUp = Vector3.Transform(cameraUp, Matrix.CreateFromAxisAngle(cameraDirection, -MathHelper.PiOver4 / 45));

            //Pitch - around X axis

            if (ks.IsKeyDown(Keys.NumPad8))
            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 45)));

            if (ks.IsKeyDown(Keys.NumPad2))
            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / -45)));


          //  cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - prevMouseState.Y)));

          //  cameraUp = Vector3.Transform(cameraUp, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - prevMouseState.Y)));

            //reset mouse state
            prevMouseState = Mouse.GetState();

            //recreate camera view
            CreateLookAt();


            base.Update(gameTime);
        }

        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }

        public float returnPosX()
        {
            return cameraPosition.X;

        }

        public float returnPosY()
        {
            return cameraPosition.Y;
        }

        public float returnPosZ()
        {
            return cameraPosition.Z;
        }

        public float returnPitch()
        {
            return -cameraDirection.Y;
        }

        public float returnRoll()
        {
            return cameraUp.Y;
        }

        public float returnYaw()
        {
            return cameraDirection.X;
        }

        public void changeCameraPos(Vector3 position, Quaternion shipRotation, GraphicsDevice device)
        {

            Vector3 campos = new Vector3(0, 3f, 8f);
            campos = Vector3.Transform(campos, Matrix.CreateFromQuaternion(shipRotation));
            campos += position;

            Vector3 camup = new Vector3(0, 1, 0);
            camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(shipRotation));

            cameraPosition = campos;
            view = Matrix.CreateLookAt(campos, position, camup);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, 0.5f, 500.0f);

        }
    }
}
