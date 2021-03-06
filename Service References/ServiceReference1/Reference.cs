﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HiddenWords.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TitleInfoServiceModel", Namespace="http://schemas.datacontract.org/2004/07/andreigecMVC.Services")]
    [System.SerializableAttribute()]
    public partial class TitleInfoServiceModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LatestTitleChangelogField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LatestTitleDownloadPathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LatestTitleVersionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TitleIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LatestTitleChangelog {
            get {
                return this.LatestTitleChangelogField;
            }
            set {
                if ((object.ReferenceEquals(this.LatestTitleChangelogField, value) != true)) {
                    this.LatestTitleChangelogField = value;
                    this.RaisePropertyChanged("LatestTitleChangelog");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LatestTitleDownloadPath {
            get {
                return this.LatestTitleDownloadPathField;
            }
            set {
                if ((object.ReferenceEquals(this.LatestTitleDownloadPathField, value) != true)) {
                    this.LatestTitleDownloadPathField = value;
                    this.RaisePropertyChanged("LatestTitleDownloadPath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LatestTitleVersion {
            get {
                return this.LatestTitleVersionField;
            }
            set {
                if ((this.LatestTitleVersionField.Equals(value) != true)) {
                    this.LatestTitleVersionField = value;
                    this.RaisePropertyChanged("LatestTitleVersion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TitleID {
            get {
                return this.TitleIDField;
            }
            set {
                if ((this.TitleIDField.Equals(value) != true)) {
                    this.TitleIDField = value;
                    this.RaisePropertyChanged("TitleID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TitleName {
            get {
                return this.TitleNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleNameField, value) != true)) {
                    this.TitleNameField = value;
                    this.RaisePropertyChanged("TitleName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IServices")]
    public interface IServices {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/GetTitleInfo", ReplyAction="http://tempuri.org/IServices/GetTitleInfoResponse")]
        HiddenWords.ServiceReference1.TitleInfoServiceModel GetTitleInfo(string titleName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicesChannel : HiddenWords.ServiceReference1.IServices, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicesClient : System.ServiceModel.ClientBase<HiddenWords.ServiceReference1.IServices>, HiddenWords.ServiceReference1.IServices {
        
        public ServicesClient() {
        }
        
        public ServicesClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServicesClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicesClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public HiddenWords.ServiceReference1.TitleInfoServiceModel GetTitleInfo(string titleName) {
            return base.Channel.GetTitleInfo(titleName);
        }
    }
}
