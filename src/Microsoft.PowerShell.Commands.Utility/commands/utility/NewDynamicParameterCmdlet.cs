// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Management.Automation;

namespace Microsoft.PowerShell.Commands
{
    /// <summary>
    /// Implements the new-dynamicparameter cmdlet.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "DynamicParameter", HelpUri = "https://go.microsoft.com/fwlink/?LinkID=2097036", RemotingCapability = RemotingCapability.None)]
    public class NewDynamicParameterCommand : PSCmdlet
    {
        internal readonly RuntimeDefinedParameter _runtimeDefinedParameter = new();

        [Parameter(Mandatory = true, Position = 0)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; set; } = string.Empty;

        [Parameter()]
        public SwitchParameter AllowNull { get; set; }

        [Parameter()]
        [AllowNull]
        public object DefaultValue { get; set; }

        [Parameter()]
        public string HelpMessage { get; set; }

        [Parameter()]
        public string ParameterSet { get; set; }

        [Parameter()]
        public SwitchParameter Hidden { get; set; }

        [Parameter()]
        public SwitchParameter Mandatory { get; set; }

        [Parameter()]
        public int Position { get; set; } = 0;

        [Parameter()]
        public Type Type { get; set; } = typeof(object);

        [Parameter()]
        public string ValidatePattern { get; set; }

        [Parameter()]
        public string ValidatePatternErrorMessage { get; set; }

        [Parameter()]
        public string[] ValidValues { get; set; }

        [Parameter()]
        public SwitchParameter ValueFromPipeline { get; set; }

        [Parameter()]
        public SwitchParameter ValueFromPipelineByPropertyName { get; set; }

        [Parameter()]
        public SwitchParameter ValueFromRemainingArguments { get; set; }

        protected override void ProcessRecord()
        {
            _runtimeDefinedParameter.Name = Name;
            _runtimeDefinedParameter.ParameterType = Type;
            _runtimeDefinedParameter.Value = DefaultValue;

            var parameterAttribute = new ParameterAttribute
            {
                Mandatory = Mandatory,
                DontShow = Hidden,
                ValueFromPipelineByPropertyName = ValueFromPipelineByPropertyName,
                ValueFromRemainingArguments = ValueFromRemainingArguments,
                Position = Position
            };

            if (!string.IsNullOrEmpty(HelpMessage))
                parameterAttribute.HelpMessage = HelpMessage;

            if (!string.IsNullOrEmpty(ParameterSet))
                parameterAttribute.ParameterSetName = ParameterSet;

            if (ValueFromPipeline)
                parameterAttribute.ValueFromPipeline = ValueFromPipeline;

            _runtimeDefinedParameter.Attributes.Add(parameterAttribute);

            if (!string.IsNullOrEmpty(ValidatePattern))
            {
                var attr = new ValidatePatternAttribute(ValidatePattern);
                if (!string.IsNullOrEmpty(ValidatePatternErrorMessage))
                    attr.ErrorMessage = ValidatePatternErrorMessage;
                _runtimeDefinedParameter.Attributes.Add(attr);
            }

            if (ValidValues != null)
                _runtimeDefinedParameter.Attributes.Add(new ValidateSetAttribute(ValidValues));

            if (AllowNull)
                _runtimeDefinedParameter.Attributes.Add(new AllowNullAttribute());

            if (DefaultValue is not null)
                _runtimeDefinedParameter.Attributes.Add(new DefaultValueAttribute(DefaultValue));
        }

        protected override void EndProcessing()
        {
            WriteObject(_runtimeDefinedParameter);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
