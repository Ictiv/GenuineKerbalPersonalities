using UnityEngine;
using Genuine_Kerbal_Personalities;

namespace Genuine_Kerbal_Personalities.Extensions
{
    public static class GKPResponses
    {
        public static string response;
        public static string CallOS(float tag, float personality, string name)
        {
            if (personality == 1f)
            {
                if (tag == -1f)
                {
                    response = (name+" here! Gosh-dolly, Isn't this a swell day to be alive?\nI'm assuming it's day. The boys forgot to give me the abillity to tell the difference.\nIt's all GREAT though!");
                    return response;
                }
                if (tag == 0f)
                {
                    response = "Updated Message";
                    return response;
                }
                if (tag == 1f)
                {
                    response = "Resource Report";
                    return response;
                }
                if (tag == 2f)
                {
                    response = "Status Report";
                    return response;
                }
                if (tag == 3f)
                {
                    response = ("Flight Report \n Mission Elapsed Time: \n " + FlightLogger.met_days + ":" + FlightLogger.met_hours + ":" + FlightLogger.met_mins + ":" + FlightLogger.met_secs);
                    return response;
                }
            }
            if (personality == 2f)
            {
                if (tag == -1f)
                {
                    response = "Brilliant... A trip into the 'great unknown'.\n\nThis is dreadful.";
                    return response;
                }
                if (tag == 0f)
                {
                    response = "Updated Message";
                    return response;
                }
                if (tag == 1f)
                {
                    response = "Resource Report";
                    return response;
                }
                if (tag == 2f)
                {
                    response = "Status Report";
                    return response;
                }
                if (tag == 3f)
                {
                    response = ("Flight Report \n Mission Elapsed Time: \n " + FlightLogger.met_days + ":" + FlightLogger.met_hours + ":" + FlightLogger.met_mins + ":" + FlightLogger.met_secs);
                    return response;
                }
            }
            if (personality == 3f)
            {
                if (tag == -1f)
                {
                    response = (name+"'s the name, and 'xploring is my game.\nSet me out on a mission and I tell you buddy, I'll follow through to the very end, or format trying!\nYou can always cound on "+name);
                    return response;
                }
                if (tag == 0f)
                {
                    response = "Updated Message";
                    return response;
                }
                if (tag == 1f)
                {
                    response = "Resource Report";
                    return response;
                }
                if (tag == 2f)
                {
                    response = "Status Report";
                    return response;
                }
                if (tag == 3f)
                {
                    response = ("Flight Report \n Mission Elapsed Time: \n " + FlightLogger.met_days + ":" + FlightLogger.met_hours + ":" + FlightLogger.met_mins + ":" + FlightLogger.met_secs);
                    return response;
                }
            }
            return response;
        }
    }
}
