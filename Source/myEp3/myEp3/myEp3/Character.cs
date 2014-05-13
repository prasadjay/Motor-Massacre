using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace myEp3
{
    class Character
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public string Name { get; set; }
        public int Car { get; set; }
        public int Mode { get; set; }
        public int Life { get; set;}
        public NetConnection Connection { get; set; }
        public Character(string name, int x, int y, int z, int car, int mode, int life, NetConnection conn)
        {
            Name = name;
            X = x;
            Y = y;
            Z = z;
            Car = car;
            mode = Mode;
            Life = life;
            Connection = conn;
        }
        public Character()
        {
        }
    }
}

/*

namespace myEp3
{
    class Character
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int car { get; set; }
        public string name { get; set; }
        public NetConnection Connection { get; set; }

        public Character(string name, int x, int y, int z, int car, NetConnection conn)
        {
            this.name = name;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.car = car;
            Connection = conn;
        }
        public Character()
        {
        }
    }
}

*/