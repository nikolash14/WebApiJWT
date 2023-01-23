using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESSModels.Model
{
    public class MailErrorLog
    {
        private string _server = string.Empty;
        private string _to = string.Empty;
        private string _from = string.Empty;
        private int _port = 0;

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public string From
        {
            get { return _from; }
            set { _from = value; }
        }


        public string To
        {
            get { return _to; }
            set { _to = value; }
        }
    }
}
