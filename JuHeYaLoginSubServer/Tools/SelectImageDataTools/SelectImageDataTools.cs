using System.Collections.Generic;
using YSF;

namespace SubServer
{
    public static class SelectImageDataTools
    {
        public static byte[] GetBytes(IList<SelectImageData> list)
        {
            if (list.IsNullOrEmpty())
            {
                return null;
            }
            IListData<byte[]> byteList = ClassPool<ListData<byte[]>>.Pop();
            for (int i = 0; i < list.Count; i++)
            {
                byteList.Add(list[i].ToBytes());
            }
            byte[] returnBytes = ByteTools.Concat(byteList.list);
            byteList.Recycle();
            return returnBytes;
        }

        public static IListData<SelectImageData> GetData(byte[] data)
        {
            if (data.IsNullOrEmpty())
            {
                return null;
            }
            int count = data.Length / SelectImageData.LEN;
            IListData<SelectImageData> list = ClassPool<ListPoolData<SelectImageData>>.Pop();
            for (int i = 0; i < count; i++)
            {
                list.Add(ConverterDataTools.ToPoolObject<SelectImageData>(data, i * SelectImageData.LEN));
            }
            return list;
        }
        public static string GetStr(IListData<SelectImageData> list)
        {
            if (list.IsNullOrEmpty()) return null;
            StringBuilderPool stringBuilderPool = ClassPool<StringBuilderPool>.Pop();
            for (int i = 0; i < list.Count; i++)
            {
                stringBuilderPool.Append(list[i].ToString());
                if (i != list.Count - 1) 
                {
                    stringBuilderPool.Append("&");
                }
            }
            string str = stringBuilderPool.ToString();
            stringBuilderPool.Recycle();
            return str;
        }

        public static byte[] GetBytes(string data)
        {
            if (data.IsNullOrEmpty()) return null;
            string[] imagesData = data.Split('&');
            IListData<SelectImageData> list = ClassPool<ListPoolData<SelectImageData>>.Pop();
            for (int i = 0; i < imagesData.Length; i++)
            {
                string[] imageMsgs = imagesData[i].Split('_');
                long name = imageMsgs[0].ToLong();
                string[] size = imageMsgs[1].Split('*');
                short sizeX = size[0].ToShort();
                short sizeY = size[1].ToShort();
                SelectImageData selectImageData = ClassPool<SelectImageData>.Pop();
                selectImageData.name = name;
                selectImageData.sizeX = sizeX;
                selectImageData.sizeY = sizeY;
                list.Add(selectImageData);
            }
            byte[] bytes = GetBytes(list.list);
            list.Recycle();
            return bytes;
        }
    }
}
