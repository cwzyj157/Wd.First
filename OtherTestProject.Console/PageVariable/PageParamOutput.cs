using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console
{
    public interface IPageBase
    {
        string Output();
    }

    public class PageBase : IPageBase
    {
        private StringBuilder sb;
        public PageBase()
        {
            sb = new StringBuilder();
        }

        public virtual IPageBase Param(string paraName, string paraValue)
        {
            Format(paraName, paraValue);
            return this;
        }
        public virtual IPageBase Params(IDictionary<string, string> dictParams)
        {
            foreach (var param in dictParams)
            {
                Format(param.Key, param.Value);
            }
            return this;
        }

        private void Format(string paraName, string paraValue)
        {
            if (string.IsNullOrEmpty(paraName)) throw new ArgumentNullException("paraName");
            if (string.IsNullOrEmpty(paraValue)) throw new ArgumentNullException("paraValue");
            if (paraValue.Length > 0)
            {
                sb.AppendFormat("var {1} = {0};", paraName, string.Format("[{0}]", paraValue.ToString()));
            }
        }

        #region IPageBase Members
        public string Output()
        {
            if (sb.Length == 0) throw new ArgumentNullException("sb");
            return string.Format("<script language=\"javascript\" type=\"text/javascript\">{0}</script>", sb.ToString());
        }
        #endregion
    }

    public class PageBaseTest
    {
        public void Test()
        {
            new PageBase().Param("aaa", "bb").Output();
        }
    }
}
