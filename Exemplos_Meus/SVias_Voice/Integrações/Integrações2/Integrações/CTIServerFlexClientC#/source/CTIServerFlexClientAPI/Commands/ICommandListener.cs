using System;
using System.Collections.Generic;
using System.Text;

namespace CTIServerFlexClientAPI.Commands
{
    public interface ICommandListener
    {
        void OnCommandResponse(Command command);
        void OnCommandTimeout(Command command);
    }
}
