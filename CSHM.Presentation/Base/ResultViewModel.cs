using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentation.Base
{
    public class ResultViewModel<T>
    {
        public MessageViewModel Message { get; set; }
        public List<T> List { get; set; }
        public T Result { get; set; }
        public int TotalCount { get; set; }
    }
}
