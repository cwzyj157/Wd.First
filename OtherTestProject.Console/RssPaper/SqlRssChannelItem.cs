using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console.RssPaper
{
    public class SqlRssChannelItem : IRssChannelItem
    {
        #region IRssChannelItem Members
        public void Handle(IEnumerable<RssChannelItem> channelItems)
        {
            if (channelItems != null)
            {
                foreach (var channelItem in channelItems)
                {
                    System.Console.WriteLine("文章标题：{0},时间：{1}", channelItem.title, channelItem.pubDate);
                }
            }
        }
        #endregion
    }
}
