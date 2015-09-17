using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine3D.Engine.Base
{
    public class ComponentManager
    {
        public List<Component> Components = new List<Component>();
        //GameObject Owner;

        public void Initialise()
        {
            Components.ForEach(c => c.Initalise());
        }

        public void Update()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].Enabled)
                    Components[i].Update();
            }
        }

        public void Draw()
        {
            foreach (RenderComponent render in Components.OfType<RenderComponent>())
            {
                if (render.Enabled)
                    render.Draw();
            }
        }

        public void Add(Component component)
        {
            if (!Components.Any(c=>c.ID == component.ID))
            {
                //set manager
                Components.Add(component);
            }
        }

        public void Remove(string id)
        {
            try
            {
                Components.RemoveAt(
                    Components.IndexOf(
                        Components.First(c => c.ID == id)));
            }
            catch
            {

            }
        }

        public Component Get(string id)
        {
            if (Components.Any(c => c.ID == id))
                return Components.First(c => c.ID == id);
            else return null;
        }
    }
}
