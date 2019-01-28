using Backend.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Command
{
    public class OpenSessionCommand
    {
        public OpenSessionCommand()
        {

        }

        public Guid Execute()
        {
            using (var dc = new WordsDataContext())
            {

            }
        }
    }
}
