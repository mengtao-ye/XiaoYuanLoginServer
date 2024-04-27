using YSF;

namespace SubServer
{
    public partial class MySqlTools
    {
        public static bool SetMyMetaSchoolData(long account,byte roleID)
        {
            MySQL add = ClassPool<MySQL>.Pop();
            add.SetData(MySQLTableData.user_metaschool_data, "account", account.ToString(),"role_id",roleID.ToString());
            bool res = mManager.Add(add);
            add.Recycle();
            return res;
        }

        public static MyMetaSchoolData GetMyMetaSchoolData(long account) 
        {
            MySQL find = ClassPool<MySQL>.Pop();
            find.SetData(MySQLTableData.user_metaschool_data,"account",account.ToString());
            MyMetaSchoolData myMetaSchoolData = mManager.FindPool<MyMetaSchoolData>(find);
            find.Recycle();
            return myMetaSchoolData;
        }
    }
}
