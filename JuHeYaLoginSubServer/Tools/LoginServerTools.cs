namespace SubServer
{
    public static class LoginServerTools
    {
        /// <summary>
        /// 将字符串的端口转换成int类型的端口
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static int ParsePoint(string point)
        {
            int pointID = -1;
            int.TryParse(point, out pointID);
            if (pointID >= ushort.MaxValue || pointID <= 0)
            {
                return pointID;
            }
            return pointID;
        }
    }
}
