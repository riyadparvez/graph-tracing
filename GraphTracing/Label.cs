using System;
using System.Collections.Generic;
using System.Text;

namespace GraphTracing
{
    internal class Label
    {
        private int id;

        public int ID 
        {
            get { return id; }
            internal set { id = value; }
        }

        public Label Root { get; set; }

        public Label(int id)
        {
            this.id = id;
            this.Root = this;
        }


        internal Label GetRoot()
        {
            if (this.Root != this)
            {
                this.Root = this.Root.GetRoot();
            }
            return this.Root;
        }

        public override bool Equals(object obj)
        {
            Label otherLabel = obj as Label;
            if (otherLabel == null)
            {
                return false;
            }
            return this.ID == otherLabel.ID;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }
    }
}