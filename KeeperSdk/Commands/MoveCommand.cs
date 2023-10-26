using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class MoveCommand : AuthenticatedCommand
    {
        public MoveCommand() : base("move")
        {
        }

        [DataMember(Name = "to_type", EmitDefaultValue = false)]
        public string toType;

        [DataMember(Name = "to_uid", EmitDefaultValue = false)]
        public string toUid;
        [DataMember(Name = "link")]
        public bool isLink;

        [DataMember(Name = "move", EmitDefaultValue = false)]
        public MoveObject[] moveObjects;

        [DataMember(Name = "transition_keys", EmitDefaultValue = false)]
        public TransitionKey[] transitionKeys;

    }
}
