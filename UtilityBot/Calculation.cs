using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace UtilityBot
{
    public class Calculation
    {
        public int Start(string userCountType, Message message) 
        {
            int sumAll = 0;
            if (userCountType == "sim")
            {
                sumAll = message.Text.Length; 

            }
            else if (userCountType == "sum")
            {
                string num = Convert.ToString(message.Text);
                var words = num.Split();
                for (int i = 0; i < words.Length; sumAll += int.Parse(words[i++])) ;
            }
            return sumAll;
        }
    }
}
