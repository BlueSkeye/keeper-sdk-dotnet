using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class KeeperApiResponse
    {
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public string result;

        [DataMember(Name = "result_code", EmitDefaultValue = false)]
        public string resultCode;

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string message;

        [DataMember(Name = "command", EmitDefaultValue = false)]
        public string command;

        public bool IsSuccess => result == "success";
    }
}
