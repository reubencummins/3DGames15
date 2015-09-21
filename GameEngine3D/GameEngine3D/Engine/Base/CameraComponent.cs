using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine3D.Engine.Base
{
    class CameraComponent : Component
    {
        //rotates world by camera transforms
        public Matrix View { get; set; }
        //connects 3D points to 2D screen space coordinates
        public Matrix Projection { get; set; }
        //how close to the camera will things be rendered
        public float NearPlane { get; set; }
        //how far from the camera will things be rendered
        public float FarPlane { get; set; }

        //what is the camera looking at
        public Vector3 Target { get; set; }
        //which way is up
        public Vector3 UpDirection { get; set; }
        
        public Vector3 CameraDirection { get; set; }

        public CameraComponent(string id,Vector3 target)
        {
            ID = id;
            Target = target;
            UpDirection = new Vector3(0, 1, 0);
        }

        public void Initialise()
        {
            NearPlane = 1;
            FarPlane = 1000;
            UpdateViewMatrix();
            //take out FOV to ini
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(80), GameUtilities.GraphicsDevice.PresentationParameters.BackBufferWidth/GameUtilities.GraphicsDevice.PresentationParameters.BackBufferHeight, NearPlane, FarPlane);
            base.Initalise();
        }

        public virtual void UpdateViewMatrix()
        {

            CameraDirection = Manager.Owner.Location - Target;

            View = Matrix.CreateLookAt(Manager.Owner.Location, Target, UpDirection);
        }
    }
}
