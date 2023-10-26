
namespace KeeperSecurity.Vault
{
    internal class ApiRecordType : IRecordType
    {
        private readonly string _uid;
        public ApiRecordType(Records.RecordType recordType)
        {
            string scopeName;
            Id = recordType.RecordTypeId;
            switch (recordType.Scope)
            {
                case Records.RecordTypeScope.RtStandard:
                    Scope = RecordTypeScope.Standard;
                    scopeName = "standard";
                    break;
                case Records.RecordTypeScope.RtEnterprise:
                    Scope = RecordTypeScope.Enterprise;
                    scopeName = "enterprise";
                    break;
                default:
                    Scope = RecordTypeScope.User;
                    scopeName = "user";
                    break;
            }
            _uid = $"{scopeName}:{Id}";
            Content = recordType.Content;
        }

        public int Id { get; }
        public RecordTypeScope Scope { get; }
        public string Content { get; }
        string IUid.Uid => _uid;
    }
}
