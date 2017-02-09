using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smeshlink海绵城市Client.DLL
{
    class Sensor
    {
        public string Feed
        {
            get { return Gateway + "/" + Node + "/" + Model; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string gateway;

        public string Gateway
        {
            get { return gateway; }
            set { gateway = value; }
        }
        private string node;

        public string Node
        {
            get { return node; }
            set { node = value; }
        }
        private string model;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
