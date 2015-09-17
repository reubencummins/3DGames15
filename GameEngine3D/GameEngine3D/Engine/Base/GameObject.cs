using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine3D.Engine.Base
{
    public delegate void StringIDHandler(string id);

    public class GameObject
    {
        public event StringIDHandler OnDestroy;
        public string ID { get; set; }
        public ComponentManager Manager { get; set; }
        public Matrix World { get; set; }
        public bool Enabled { get; set; }

        public Vector3 Location { get { return World.Translation; } }
        public Vector3 Scale { get { return World.Scale; } }
        public Quaternion Rotation { get { return World.Rotation; } }

        public GameObject(string id)
        {
            ID = id;
            Manager = new ComponentManager(this);
        }

        public void Initialise()
        {
            Manager.Initialise();
        }

        public void Update()
        {
            if (Enabled)
                Manager.Update();
        }

        public void Draw()
        {
            if (Enabled)
                Manager.Draw();
        }

        public void Destroy()
        {
            if (OnDestroy != null)
                OnDestroy(ID);
        }
    }
}
