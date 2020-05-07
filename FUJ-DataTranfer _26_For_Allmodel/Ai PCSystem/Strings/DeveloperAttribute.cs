using System;

//https://www.codeproject.com/Articles/827091/Csharp-Attributes-in-minutes

namespace Ai_PCSystem.Strings
{
    [AttributeUsage(AttributeTargets.All)]
    public class Developer : Attribute
    {
        // Private fields.
        private string name;
        private string level;
        private bool reviewed;

        // This constructor defines two required parameters: name and level.

        public Developer(string name, string level)
        {
            this.name = name;
            this.level = level;
            this.reviewed = false;
        }

        // Define Name property.
        // This is a read-only attribute.

        public virtual string Name
        {
            get { return name; }
        }

        // Define Level property.
        // This is a read-only attribute.

        public virtual string Level
        {
            get { return level; }
        }

        // Define Reviewed property.
        // This is a read/write attribute.

        public virtual bool Reviewed
        {
            get { return reviewed; }
            set { reviewed = value; }
        }
    }
}