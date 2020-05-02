using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class ConfigHelper
    {
        public static T Get<T>(string key)   // ne tip verirsen geriye o tip döndersin. Port numarası int diğer alanlar string olduğu için sabit bir tip yoktur. Bu yüzden generic yaptık.
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T)); // AppSettins ten aldığım değeri Get metodunda ki tip ne ise o tipi geri dönderdik.
        }
    }
}
