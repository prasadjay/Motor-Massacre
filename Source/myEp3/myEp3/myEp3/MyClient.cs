using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace myEp3
{
    class MyClient
    {
        static NetClient Client;
        public static List<Character> GameStateList;
        static System.Timers.Timer update;

        enum MoveDirection
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            NONE
        }

        enum PacketTypes
        {
            LOGIN,
            MOVE,
            WORLDSTATE
        }

        public MyClient()
        {
            //Console.WriteLine("Enter IP To Connect");
            string hostip = "localhost";
            NetPeerConfiguration Config = new NetPeerConfiguration("game");
            Client = new NetClient(Config);
            NetOutgoingMessage outmsg = Client.CreateMessage();
            Client.Start();
            outmsg.Write((byte)PacketTypes.LOGIN);
            outmsg.Write("MyName");
            Client.Connect(hostip, 14242,outmsg);
            //Console.WriteLine("Client Started");
            GameStateList = new List<Character>();
            update = new System.Timers.Timer(50);
            update.Elapsed += new System.Timers.ElapsedEventHandler(update_Elapsed);
            WaitForStartingInfo();
            update.Start();
            
        }

        public static void update_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckServerMessages();
            GetInputAndSendItToServer();
            //DrawGameState();
        }

        private static void WaitForStartingInfo()
        {
            bool CanStart = false;
            NetIncomingMessage inc;

            while (!CanStart)
            {
                if ((inc = Client.ReadMessage()) != null)
                {
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                         
                            if (inc.ReadByte() == (byte)PacketTypes.WORLDSTATE)
                            {
                                GameStateList.Clear();
                                int count = 0;
                                count = inc.ReadInt32();
                                for (int i = 0; i < count; i++)
                                {
                                    Character ch = new Character();
                                    inc.ReadAllProperties(ch);
                                    GameStateList.Add(ch);
                                }
                                CanStart = true;
                            }
                            break;

                        default:
                            //Console.WriteLine(inc.ReadString() + " Strange message");
                            break;
                    }
                }
            }
        }

        private static void CheckServerMessages()
        {
            NetIncomingMessage inc;
            while ((inc = Client.ReadMessage()) != null)
            {
                if (inc.MessageType == NetIncomingMessageType.Data)
                {
                    if (inc.ReadByte() == (byte)PacketTypes.WORLDSTATE)
                    {
                        //Console.WriteLine("World State uppaus");
                        GameStateList.Clear();
                        int jii = 0;
                        jii = inc.ReadInt32();
                        for (int i = 0; i < jii; i++)
                        {
                            Character ch = new Character();
                            inc.ReadAllProperties(ch);
                            GameStateList.Add(ch);
                        }
                    }
                }
            }
        }

        public static void GetInputAndSendItToServer() //update loop
        {
            //NetOutgoingMessage outmsg = Client.CreateMessage();
            //outmsg.Write((byte)PacketTypes.MOVE);
            //outmsg.Write();
            //Client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);

                //MoveDirection MoveDir = new MoveDirection();

                //MoveDir = MoveDirection.NONE;

                ////ConsoleKeyInfo kinfo = Console.ReadKey();

                //KeyboardState ks = Keyboard.GetState();

                //if (ks.IsKeyDown(Keys.W))
                //    MoveDir = MoveDirection.UP;
                //if (ks.IsKeyDown(Keys.S))
                //    MoveDir = MoveDirection.DOWN;
                //if (ks.IsKeyDown(Keys.A))
                //    MoveDir = MoveDirection.LEFT;
                //if (ks.IsKeyDown(Keys.D))
                //    MoveDir = MoveDirection.RIGHT;

                //if (ks.IsKeyDown(Keys.Q))
                //{
                //   // Client.Disconnect("bye bye");
                //}

                //if (MoveDir != MoveDirection.NONE)
                //{
                //    NetOutgoingMessage outmsg = Client.CreateMessage();
                //    outmsg.Write((byte)PacketTypes.MOVE);
                //    outmsg.Write((byte)MoveDir);
                //    Client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
                //    MoveDir = MoveDirection.NONE;
                //}
           

        }

        private static void DrawGameState()
        {
            
            foreach (Character ch in GameStateList)
            {
                
            }
        }

        
    }



    
}


/*
namespace GameClient
{
    class MyClient
    {
        static NetClient Client;
        static List<Character> GameStateList;
        static System.Timers.Timer update;
        static bool IsRunning = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter IP To Connect");
            string hostip = Console.ReadLine();
            NetPeerConfiguration Config = new NetPeerConfiguration("game");
            Client = new NetClient(Config);
            NetOutgoingMessage outmsg = Client.CreateMessage();
            Client.Start();
            outmsg.Write((byte)PacketTypes.LOGIN);
            outmsg.Write("MyName");
            Client.Connect(hostip, 14242,outmsg);
            Console.WriteLine("Client Started");
            GameStateList = new List<Character>();
            update = new System.Timers.Timer(50);
            update.Elapsed += new System.Timers.ElapsedEventHandler(update_Elapsed);
            WaitForStartingInfo();
            update.Start();
            while (IsRunning)
            {
                GetInputAndSendItToServer();

            }

            
        }

        static void update_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckServerMessages();
            DrawGameState();
        }

        private static void WaitForStartingInfo()
        {
            bool CanStart = false;
            NetIncomingMessage inc;

            while (!CanStart)
            {
                if ((inc = Client.ReadMessage()) != null)
                {
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                         
                            if (inc.ReadByte() == (byte)PacketTypes.WORLDSTATE)
                            {
                                GameStateList.Clear();
                                int count = 0;
                                count = inc.ReadInt32();
                                for (int i = 0; i < count; i++)
                                {
                                    Character ch = new Character();
                                    inc.ReadAllProperties(ch);
                                    GameStateList.Add(ch);
                                }
                                CanStart = true;
                            }
                            break;

                        default:
                            Console.WriteLine(inc.ReadString() + " Strange message");
                            break;
                    }
                }
            }
        }

        private static void CheckServerMessages()
        {
            NetIncomingMessage inc;
            while ((inc = Client.ReadMessage()) != null)
            {
                if (inc.MessageType == NetIncomingMessageType.Data)
                {
                    if (inc.ReadByte() == (byte)PacketTypes.WORLDSTATE)
                    {
                        Console.WriteLine("World State uppaus");
                        GameStateList.Clear();
                        int jii = 0;
                        jii = inc.ReadInt32();
                        for (int i = 0; i < jii; i++)
                        {
                            Character ch = new Character();
                            inc.ReadAllProperties(ch);
                            GameStateList.Add(ch);
                        }
                    }
                }
            }
        }

        private static void GetInputAndSendItToServer()
        {
            MoveDirection MoveDir = new MoveDirection();
            
            MoveDir = MoveDirection.NONE;

            ConsoleKeyInfo kinfo = Console.ReadKey();
            
            if (kinfo.KeyChar == 'w')
                MoveDir = MoveDirection.UP;
            if (kinfo.KeyChar == 's')
                MoveDir = MoveDirection.DOWN;
            if (kinfo.KeyChar == 'a')
                MoveDir = MoveDirection.LEFT;
            if (kinfo.KeyChar == 'd')
                MoveDir = MoveDirection.RIGHT;

            if (kinfo.KeyChar == 'q')
            {
                Client.Disconnect("bye bye");
            }

            if (MoveDir != MoveDirection.NONE)
            {
                NetOutgoingMessage outmsg = Client.CreateMessage();
                outmsg.Write((byte)PacketTypes.MOVE);
                outmsg.Write((byte)MoveDir);
                Client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
                MoveDir = MoveDirection.NONE;
            }

        }

        enum MoveDirection
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            NONE
        }

        private static void DrawGameState()
        {
            Console.Clear();
            Console.WriteLine("###############################################################################");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("#.............................................................................#");
            Console.WriteLine("###############################################################################");
            Console.WriteLine("Move with: WASD");
            Console.WriteLine("Connections status: " + (NetConnectionStatus)Client.ServerConnection.Status);

            foreach (Character ch in GameStateList)
            {
                Console.SetCursorPosition(ch.X, ch.Y);
                Console.Write("@");
            }
        }
    }


    class Character
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public NetConnection Connection { get; set; }
        public Character(string name, int x, int y, NetConnection conn)
        {
            Name = name;
            X = x;
            Y = y;
            Connection = conn;
        }
        public Character()
        {
        }
    }


    enum PacketTypes
    {
        LOGIN,
        MOVE,
        WORLDSTATE
    }
}


*/