using YSF;

namespace SubServer
{
    public partial class MySqlTools
    {
        public static bool SetMyMetaSchoolData(long account, int roleID)
        {
            MySQL exit = ClassPool<MySQL>.Pop();
            exit.SetData(MySQLTableData.user_metaschool_data, "account", account.ToString());
            bool isExist = mManager.IsExist(exit);
            exit?.Recycle();
            bool res = false;
            if (isExist)
            {
                WhereMySQL whereMySQL = ClassPool<WhereMySQL>.Pop();
                whereMySQL.SetData(MySQLTableData.user_metaschool_data, "role_id", roleID.ToString(), "account", account.ToString());
                res = mManager.Update(whereMySQL);
                whereMySQL?.Recycle();
            }
            else
            {
                MySQL add = ClassPool<MySQL>.Pop();
                add.SetData(MySQLTableData.user_metaschool_data, "account", account.ToString(), "role_id", roleID.ToString());
                res = mManager.Add(add);
                add?.Recycle();
            }
            return res;
        }

        public static MyMetaSchoolData GetMyMetaSchoolData(long account)
        {
            MySQL find = ClassPool<MySQL>.Pop();
            find.SetData(MySQLTableData.user_metaschool_data, "account", account.ToString());
            MyMetaSchoolData myMetaSchoolData = mManager.FindPool<MyMetaSchoolData>(find);
            find.Recycle();
            return myMetaSchoolData;
        }
    }
}
