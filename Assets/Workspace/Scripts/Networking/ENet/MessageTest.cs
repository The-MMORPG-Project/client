using Common.Networking.Message;
using Common.Networking.IO;

namespace GameClient.Networking
{
    public class MessageTest : IWritable
    {
        public void Write(PacketWriter writer)
        {
            writer.Write("One");
            writer.Write("Two");
            writer.Write((byte) 3);
        }
    }
}