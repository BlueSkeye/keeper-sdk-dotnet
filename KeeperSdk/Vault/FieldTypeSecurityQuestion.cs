using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// "securityQuestion" field type
    /// </summary>
    [DataContract]
    public class FieldTypeSecurityQuestion : IFieldTypeSerialize
    {
        /// <exclude />
        public FieldTypeSecurityQuestion()
        {
            Question = "";
            Answer = "";
        }

        /// <summary>
        /// Gets or sets Security Question
        /// </summary>
        [DataMember(Name = "question", EmitDefaultValue = true)]
        public string Question { get; set; }
        /// <summary>
        /// Gets or sets Security Answer
        /// </summary>
        [DataMember(Name = "answer", EmitDefaultValue = true)]
        public string Answer { get; set; }

        private static readonly string[] QAElements = new[] { "question", "answer" };
        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.Elements => QAElements;

        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.ElementValues => new[] { Question, Answer };


        /// <exclude />
        bool IFieldTypeSerialize.SetElementValue(string element, string value)
        {
            if (element == "question")
            {
                Question = value;
            }
            else if (element == "answer")
            {
                Answer = value;
            }
            else
            {
                return false;
            }
            return true;
        }

    }
}
