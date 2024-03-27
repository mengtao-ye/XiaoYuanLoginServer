using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        /// <summary>
        /// 发表朋友圈
        /// </summary>
        /// <param name="account"></param>
        /// <param name="content"></param>
        /// <param name="images"></param>
        /// <param name="isSchool"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool PublishCampusCircle(long account,string content,string images,bool isSchool,long time) {
            MySQL add = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("account", account.ToString());
            dict.Add("content", content);
            dict.Add("images", images);
            dict.Add("is_school", isSchool ? "1" : "0");
            dict.Add("time", time.ToString());
            add.SetData( MySQLTableData.campus_circle, dict);
            bool res = mManager.Add(add);
            add.Recycle();
            return res;
        }
    }
}
