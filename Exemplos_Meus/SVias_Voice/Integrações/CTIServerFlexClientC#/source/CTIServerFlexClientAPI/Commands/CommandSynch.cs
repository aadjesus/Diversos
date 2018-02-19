using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CTIServerFlexClientAPI.Commands
{
    public class CommandSynch : ICommandListener
    {
        private CTIServerFlexClientAPI api = null;
        public CommandSynch(CTIServerFlexClientAPI api)
        {
            this.api = api;
        }
        public void OnCommandResponse(Command command) {
    	    lock(this) {
                Monitor.PulseAll(this);
    	    }
        }
        public void OnCommandTimeout(Command command) {
    	    lock(this) {
    		    Monitor.PulseAll(this);
    	    }
        }
        public int Execute(Command command)
        {
            command.AddListener(this);
            lock(this) {
	            try {
	        	    api.Execute(command);
				    Monitor.Wait(this);
			    } catch (Exception e) {
                    System.Console.WriteLine(e.StackTrace);
				    return -1;
			    }
            }
            return command.Response.Response;
        }
    }
}
