using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine3D.Engine.Base
{
    public class Component
    {
        public string ID { get; set; }
        public bool Enabled { get; set; }

        public virtual void Initalise() { }
        public virtual void Update() { }
        public virtual void Destroy()
        {

        }
    }
}
