using System.Collections.Generic;

namespace BatiFren.Entities.EntityClasses
{
    public class Menu
    {
        public int id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public virtual List<Entities.EntityClasses.Menu> Children { get; set; }
    }
}
