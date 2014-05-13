using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace myEp3
{
    class MyServer
    {
        static NetServer Server;
        static NetPeerConfiguration Config;
        static NetIncomingMessage inc;
        static List<Character> GameWorldState = new List<Character>();
        static DateTime time = DateTime.Now;
        static TimeSpan timetopass = new TimeSpan(0, 0, 0, 0, 30);
        static TimeSpan timeToServerChange = new TimeSpan(0, 1, 0);

        enum PacketTypes
        {
            LOGIN,
            MOVE,
            WORLDSTATE
        }

        enum MoveDirection
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            NONE
        }

        public MyServer()
        {
            Config = new NetPeerConfiguration("game");
            Config.Port = 14242;
            Config.MaximumConnections = 10;

            Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            Server = new NetServer(Config);
            Server.Start();
        }

           public void updateServer()
            {
                if ((inc = Server.ReadMessage()) != null)
                {
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.ConnectionApproval:

                            if (inc.ReadByte() == (byte)PacketTypes.LOGIN)
                            {
                                //Console.WriteLine("Incoming LOGIN");
                                inc.SenderConnection.Approve();
                                Random r = new Random();
                                GameWorldState.Add(new Character(inc.ReadString(), 2, 2, 2, 1, 1, 100, inc.SenderConnection));
                                NetOutgoingMessage outmsg = Server.CreateMessage();
                                outmsg.Write((byte)PacketTypes.WORLDSTATE);
                                outmsg.Write(GameWorldState.Count);
                                foreach (Character ch in GameWorldState)
                                {
                                    outmsg.WriteAllProperties(ch);
                                }

                                Server.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                                //Console.WriteLine("Approved new connection and updated the world status");
                            }

                            break;
                        // Data type is all messages manually sent from client
                        // ( Approval is automated process )
                        case NetIncomingMessageType.Data:

                            // Read first byte
                            if (inc.ReadByte() == (byte)PacketTypes.MOVE)
                            {
                                // Check who sent the message
                                // This way we know, what character belongs to message sender
                                foreach (Character ch in GameWorldState)
                                {
                                    // If stored connection ( check approved message. We stored ip+port there, to character obj )
                                    // Find the correct character
                                    if (ch.Connection != inc.SenderConnection)
                                        continue;

                                    // Read next byte
                                    byte b = inc.ReadByte();

                                    // Handle movement. This byte should correspond to some direction
                                    if ((byte)MoveDirection.UP == b)
                                        ch.Y--;
                                    if ((byte)MoveDirection.DOWN == b)
                                        ch.Y++;
                                    if ((byte)MoveDirection.LEFT == b)
                                        ch.X--;
                                    if ((byte)MoveDirection.RIGHT == b)
                                        ch.X++;

                                    NetOutgoingMessage outmsg = Server.CreateMessage();
                                    outmsg.Write((byte)PacketTypes.WORLDSTATE);
                                    outmsg.Write(GameWorldState.Count);
                                    foreach (Character ch2 in GameWorldState)
                                    {
                                        outmsg.WriteAllProperties(ch2);
                                    }
                                    Server.SendMessage(outmsg, Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                                    break;
                                }

                            }
                            break;
                        case NetIncomingMessageType.StatusChanged:

                            // NOTE: Disconnecting and Disconnected are not instant unless client is shutdown with disconnect()
                            //Console.WriteLine(inc.SenderConnection.ToString() + " status changed. " + (NetConnectionStatus)inc.SenderConnection.Status);
                            if (inc.SenderConnection.Status == NetConnectionStatus.Disconnected || inc.SenderConnection.Status == NetConnectionStatus.Disconnecting)
                            {
                                // Find disconnected character and remove it
                                foreach (Character cha in GameWorldState)
                                {
                                    if (cha.Connection == inc.SenderConnection)
                                    {
                                        GameWorldState.Remove(cha);
                                        break;
                                    }
                                }
                            }
                            break;
                        default:
                            // As i statet previously, theres few other kind of messages also, but i dont cover those in this example
                            // Uncommenting next line, informs you, when ever some other kind of message is received
                            //Console.WriteLine("Not Important Message");
                            break;
                    }
                } // If New messages

                // if 30ms has passed
                if ((time + timetopass) < DateTime.Now)
                {
                    // If there is even 1 client
                    if (Server.ConnectionsCount != 0)
                    {
                        // Create new message
                        NetOutgoingMessage outmsg = Server.CreateMessage();

                        // Write byte
                        outmsg.Write((byte)PacketTypes.WORLDSTATE);

                        // Write Int
                        outmsg.Write(GameWorldState.Count);

                        // Iterate throught all the players in game
                        foreach (Character ch2 in GameWorldState)
                        {

                            // Write all properties of character, to the message
                            outmsg.WriteAllProperties(ch2);
                        }

                        // Message contains
                        // byte = Type
                        // Int = Player count
                        // Character obj * Player count

                        // Send messsage to clients ( All connections, in reliable order, channel 0)
                        Server.SendMessage(outmsg, Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                    }
   
                    time = DateTime.Now;
                }

               //if its time to change the server

                if ((time + timeToServerChange) < DateTime.Now)
                {
                    // If there is even 1 client
                    if (Server.ConnectionsCount != 0)
                    {
                        // Create new message
                        NetOutgoingMessage outmsg = Server.CreateMessage();

                        // Write byte
                        outmsg.Write((byte)PacketTypes.WORLDSTATE);

                        // Write Int
                        outmsg.Write(GameWorldState.Count);

                        // Iterate throught all the players in game
                        foreach (Character ch2 in GameWorldState)
                        {

                            // Write all properties of character, to the message
                            outmsg.WriteAllProperties(ch2);
                        }

                        // Message contains
                        // byte = Type
                        // Int = Player count
                        // Character obj * Player count

                        // Send messsage to clients ( All connections, in reliable order, channel 0)
                        Server.SendMessage(outmsg, Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                    }

                    time = DateTime.Now;
                }
              
               
            }
    }
}

/*
namespace myEp3
{
    class MyServer
    {
        static NetServer Server;
        static NetPeerConfiguration Config;
        static NetIncomingMessage inc;
        static List<Character> GameWorldState = new List<Character>();
        static DateTime time = DateTime.Now;
        static TimeSpan timetopass = new TimeSpan(0, 0, 0, 0, 30);

        enum PacketTypes
        {
            LOGIN,
            MOVE,
            WORLDSTATE
        }

        enum MoveDirection
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            NONE
        }

        public MyServer()
        {
            Config = new NetPeerConfiguration("game");
            Config.Port = 14242;
            Config.MaximumConnections = 10;

            Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            Server = new NetServer(Config);
            Server.Start();
        }

           public void updateServer()
            {
                if ((inc = Server.ReadMessage()) != null)
                {
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.ConnectionApproval:

                            if (inc.ReadByte() == (byte)PacketTypes.LOGIN)
                            {
                                //Console.WriteLine("Incoming LOGIN");
                                inc.SenderConnection.Approve();
                                Random r = new Random();
                                GameWorldState.Add(new Character(inc.ReadString(), 5, 5, 5, 2, inc.SenderConnection));
                                NetOutgoingMessage outmsg = Server.CreateMessage();
                                outmsg.Write((byte)PacketTypes.WORLDSTATE);
                                outmsg.Write(GameWorldState.Count);
                                foreach (Character ch in GameWorldState)
                                {
                                    outmsg.WriteAllProperties(ch);
                                }

                                Server.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                                //Console.WriteLine("Approved new connection and updated the world status");
                            }

                            break;
                        // Data type is all messages manually sent from client
                        // ( Approval is automated process )
                        case NetIncomingMessageType.Data:

                            // Read first byte
                            if (inc.ReadByte() == (byte)PacketTypes.MOVE)
                            {
                                // Check who sent the message
                                // This way we know, what character belongs to message sender
                                foreach (Character ch in GameWorldState)
                                {
                                    // If stored connection ( check approved message. We stored ip+port there, to character obj )
                                    // Find the correct character
                                    if (ch.Connection != inc.SenderConnection)
                                        continue;

                                    // Read next byte
                                    byte b = inc.ReadByte();

                                    // Handle movement. This byte should correspond to some direction
                                    if ((byte)MoveDirection.UP == b)
                                        ch.Y--;
                                    if ((byte)MoveDirection.DOWN == b)
                                        ch.Y++;
                                    if ((byte)MoveDirection.LEFT == b)
                                        ch.X--;
                                    if ((byte)MoveDirection.RIGHT == b)
                                        ch.X++;

                                    NetOutgoingMessage outmsg = Server.CreateMessage();
                                    outmsg.Write((byte)PacketTypes.WORLDSTATE);
                                    outmsg.Write(GameWorldState.Count);
                                    foreach (Character ch2 in GameWorldState)
                                    {
                                        outmsg.WriteAllProperties(ch2);
                                    }
                                    Server.SendMessage(outmsg, Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                                    break;
                                }

                            }
                            break;
                        case NetIncomingMessageType.StatusChanged:

                            // NOTE: Disconnecting and Disconnected are not instant unless client is shutdown with disconnect()
                            //Console.WriteLine(inc.SenderConnection.ToString() + " status changed. " + (NetConnectionStatus)inc.SenderConnection.Status);
                            if (inc.SenderConnection.Status == NetConnectionStatus.Disconnected || inc.SenderConnection.Status == NetConnectionStatus.Disconnecting)
                            {
                                // Find disconnected character and remove it
                                foreach (Character cha in GameWorldState)
                                {
                                    if (cha.Connection == inc.SenderConnection)
                                    {
                                        GameWorldState.Remove(cha);
                                        break;
                                    }
                                }
                            }
                            break;
                        default:
                            // As i statet previously, theres few other kind of messages also, but i dont cover those in this example
                            // Uncommenting next line, informs you, when ever some other kind of message is received
                            //Console.WriteLine("Not Important Message");
                            break;
                    }
                } // If New messages

                // if 30ms has passed
                if ((time + timetopass) < DateTime.Now)
                {
                    // If there is even 1 client
                    if (Server.ConnectionsCount != 0)
                    {
                        // Create new message
                        NetOutgoingMessage outmsg = Server.CreateMessage();

                        // Write byte
                        outmsg.Write((byte)PacketTypes.WORLDSTATE);

                        // Write Int
                        outmsg.Write(GameWorldState.Count);

                        // Iterate throught all the players in game
                        foreach (Character ch2 in GameWorldState)
                        {

                            // Write all properties of character, to the message
                            outmsg.WriteAllProperties(ch2);
                        }

                        // Message contains
                        // byte = Type
                        // Int = Player count
                        // Character obj * Player count

                        // Send messsage to clients ( All connections, in reliable order, channel 0)
                        Server.SendMessage(outmsg, Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                    }
   
                    time = DateTime.Now;
                }

            }
    }
}


*/