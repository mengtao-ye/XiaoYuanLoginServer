using System;
using System.Collections.Generic;
using YSF;

namespace SubServer
{
    public class TokenManager : Singleton<TokenManager>
    {
        private IList<long> mTokenList;
        private  Random mRandom;
        public TokenManager()
        {
            mTokenList = new List<long>();
            mRandom = new Random();
        }
        /// <summary>
        /// 移除Token
        /// </summary>
        public void RemoveToken(long token)
        {
            if (mTokenList.Contains(token))
            {
                mTokenList.Remove(token);
            }    
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool VarifyToken(long token) 
        {
            return mTokenList.Contains(token);
        }

        /// <summary>
        /// 创建Token
        /// </summary>
        /// <returns></returns>
        public long CreateToken() 
        {
            long token = CreateLongValue();
            while (mTokenList.Contains(token))
            {
                token = CreateLongValue();
            }
            mTokenList.Add(token);
            return token;
        }

        private long CreateLongValue()
        {
            long maxValue = long.MaxValue;
            long minValue = 1;
            byte[] buf = new byte[8];
            mRandom.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            long absLongRand = Math.Abs(longRand);
            long randomLong = longRand < 0 ? (longRand % (maxValue - minValue)) - (absLongRand % (maxValue - minValue)) : longRand % (maxValue - minValue) + minValue;
            return randomLong;
        }
    }
}
