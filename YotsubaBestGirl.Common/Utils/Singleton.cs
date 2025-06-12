using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YotsubaBestGirl.Common.Utils
{
    public abstract class Singleton<T> where T : new()
    {
        static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = new T();

                return instance;
            }
            set => instance = value;
        }
    }
}
