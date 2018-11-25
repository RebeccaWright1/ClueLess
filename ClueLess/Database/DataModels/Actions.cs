using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class Actions
    {
        public int ID { get; set; }
        public string ActionName { get; set; }

        public virtual ICollection<ActionTaken> ActionsTaken { get; set; }
    }
}