using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SubscriptionService1
{
    class AppEngine
    {
         private static AppEngine _instance;
        private static IUnityContainer _container;
        private static HtmlTextWriter _htmlTextWriter;
        private AppEngine()
        {
            if (_container == null) _container = new UnityContainer();
            if (_htmlTextWriter != null) return;
            var stringBuilder = new StringBuilder();
            var textWriter = new StringWriter(stringBuilder);
            _htmlTextWriter = new HtmlTextWriter(textWriter);
        }

        public IUnityContainer Container { get { return _container; } set { _container = value; } }

        public HtmlTextWriter HtmlTextWriter { get { return _htmlTextWriter; } }

        public static AppEngine Instance
        {
            get { return _instance ?? (_instance = new AppEngine()); }
        }
    }
}
