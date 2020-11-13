using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class User: BaseModel
    {
        public int Code { get; set; }
    }
    public class CreateUserViewModel
    {
        public int Code { get; set; }
    }
}

