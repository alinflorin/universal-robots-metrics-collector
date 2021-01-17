using System.Collections.Generic;

namespace IMS.URM.Dto.Abstractions
{
    public class BaseDto
    {
        public bool OperationSuccessful { get; set; } = true;
        public IEnumerable<string> Errors { get; set; } = new HashSet<string>();
    }
}
