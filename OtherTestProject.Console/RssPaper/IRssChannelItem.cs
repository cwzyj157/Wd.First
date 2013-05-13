using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console.RssPaper
{
    public interface IRssChannelItem
    {
        void Handle(IEnumerable<RssChannelItem> channelItems);
    }
}
