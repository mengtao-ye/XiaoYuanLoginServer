using YSF;

namespace SubServer
{
    public static class SocketTools
    {
        /// <summary>
        /// 验证客户端发来的数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] VarifyClientData(byte[] data)
        {
            if (data.IsNullOrEmpty())
            {
                Debug.LogError("客户端发来的数据格式错误");
                return null;
            }
            long token = data.ToLong();
            bool res = TokenManager.Instance.VarifyToken(token);
            if (res)
            {
                return ByteTools.SubBytes(data, 8);
            }
            else
            {
                Debug.LogError("Token不存在:"+ token);
                return null;
            }
        }

        public static byte[] GetNullParamError()
        {
            return ToBytes(SocketResultCode.NullParamError, "传递来的值为空", null);
        }

        public static byte[] ToBytes(SocketResultCode resultCode, string msg, byte[] data)
        {
            return ToBytes((byte)resultCode, msg, data);
        }

        public static byte[] ToBytes(byte resultCode, string msg, byte[] data)
        {
            IListData<byte[]> list = ClassPool<ListData<byte[]>>.Pop();
            list.Add(resultCode.ToBytes());
            list.Add(msg.ToBytes());
            list.Add(data);
            byte[] bytes = list.list.ToBytes();
            list.Recycle();
            return bytes;
        }
        public static SocketReturnMsg ToValue(byte[] datas)
        {
            return ConverterDataTools.ToPoolObject<SocketReturnMsg>(datas);
        }
    }
}
