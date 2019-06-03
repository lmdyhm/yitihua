using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tool
{
   public static  class SerializeHelper
    {
        public static string SerializeObject(object o)
        {
            System.Runtime.Serialization.IFormatter obj = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            MemoryStream ms = new MemoryStream();
            obj.Serialize(ms, o);

            BinaryReader br = new BinaryReader(ms);
            ms.Position = 0;
            byte[] bs = br.ReadBytes((int)ms.Length);
            ms.Close();
            return Convert.ToBase64String(bs);


        }

        public static object DeserializeObject(string str)
        {
            System.Runtime.Serialization.IFormatter obj = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            byte[] bs = Convert.FromBase64String(str);
            MemoryStream ms = new MemoryStream();
            ms.Write(bs, 0, bs.Length);
            ms.Position = 0;
            object o = obj.Deserialize(ms);
            ms.Close();
            return o;


        }

    }
}
