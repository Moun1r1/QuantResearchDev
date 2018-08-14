using Moon.Nodes.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Moon.Nodes.shared
{
    public static class Static
    {
        public static WebSocketServer wssv { get; set; } = new WebSocketServer(1346);
        public static NodeManager Manager { get; set; } = new NodeManager();

    }
}
