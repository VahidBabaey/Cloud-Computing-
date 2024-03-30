using Kavenegar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCore.Classes
{
   public class MessageSender
    {
        public void SMS(string to, string body)
        {
            var sender = "09374807400";
            var receptor = to;
            var message = body;
            var api = new KavenegarApi("437332596236674966525038677074597134735739734C564E334876656B7267346961704A7452677643343D");
            api.Send(sender, receptor, message);

        }
    }
}
