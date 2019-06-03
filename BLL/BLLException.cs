using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BLLException :Exception
    {
        private string msg;

        public BLLException(string msg)
        {
            this.msg = msg;
        }

        public string Msg
        {
            get { return this.msg; }
        }
    }
}
