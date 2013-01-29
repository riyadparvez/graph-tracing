using System;
using System.Collections.Generic;
using System.Text;

namespace GraphTracing
{
    internal class Label
    {
        public int ID { get; set; }

        public Label Root { get; set; }

        public Label(int id)
        {
            this.ID = id;
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
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            Label other = (Label)obj;
            return this.ID == other.ID;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }
    }
}