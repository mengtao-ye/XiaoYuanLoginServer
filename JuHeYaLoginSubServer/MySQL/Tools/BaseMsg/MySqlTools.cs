using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        private static MySQLManager mManager;
        private static bool IsInit
        {
            get
            {
                return mManager != null;
            }
        }
        /// <summary>
        /// 初始化MySQl管理器
        /// </summary>
        /// <param name="manager"></param>
        public static void InitMySQLManager(MySQLManager manager)
        {
            mManager = manager;
        }
    }
}
