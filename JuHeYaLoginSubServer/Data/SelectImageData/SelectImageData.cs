using YSF;

namespace SubServer
{
    public class SelectImageData : IDataConverter, IPool
    {
        public long name;
        public short sizeX;
        public short sizeY;
        public bool isPop { get; set; }
        public const int LEN = 12;
        public void PopPool() { }
        public void PushPool() { }
        public void Recycle()
        {
            ClassPool<SelectImageData>.Push(this);
        }
        public byte[] ToBytes()
        {
            return ByteTools.ConcatParam(name.ToBytes(), sizeX.ToBytes(), sizeY.ToBytes());
        }
        public void ToValue(byte[] data)
        {
            name = data.ToLong(0);
            sizeX = data.ToShort(8);
            sizeY = data.ToShort(10);
        }
        public override string ToString()
        {
            return name.ToString()+"_"+ sizeX.ToString()+"*"+ sizeY.ToString();
        }
    }
}
