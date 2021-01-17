using IMS.URM.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.BusinessServices.Abstractions
{
    public interface ITcpDecoderService
    {
        RobotEventDto Decode(string data);
    }
}
