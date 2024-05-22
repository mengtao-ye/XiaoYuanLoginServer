using YSF;

namespace SubServer
{
    public static class MySQLStr
    {
        /// <summary>
        /// 获取模糊查找MySQL语句
        /// </summary>
        /// <param name="str"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetLikeStr(string str,string field)
        {
            if (str.IsNullOrEmpty()) return "";
            string[] strs = str.Split();
            StringBuilderPool stringBuilderPool = ClassPool<StringBuilderPool>.Pop();

            for (int i = 0; i < strs.Length; i++)
            {
                stringBuilderPool.Append($" {field} like '%{strs[i]}%'");
                if (i != strs.Length - 1)
                {
                    stringBuilderPool.Append(" or ");
                }
            }
            string returnStr = stringBuilderPool.ToString();
            stringBuilderPool.Recycle();
            return returnStr;
        }
    }
}
