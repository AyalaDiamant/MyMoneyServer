using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class IdName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //   public bool? IsGlobal { get; set; }
        public int? ManagerId { get; set; }
        public bool? IsActive { get; set; }

        public int? Type { get; set; }

    }

    public class IdNameDB : IdName
    {
        public TableCode TableCode { get; set; }
    }

    public class Result
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public Result(bool success = true)
        {
            this.Success = success;
        }
    }

    public class GResult<T> : Result
    {
        public T Value { get; set; }
    }
}
