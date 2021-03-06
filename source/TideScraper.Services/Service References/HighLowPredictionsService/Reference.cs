﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TideScraper.Services.HighLowPredictionsService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl", ConfigurationName="HighLowPredictionsService.highlowtidepredPortType")]
    public interface highlowtidepredPortType {
        
        // CODEGEN: Generating message contract since the operation getHighLowTidePredictions is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        TideScraper.Services.HighLowPredictionsService.getHighLowTidePredictionsResponse getHighLowTidePredictions(TideScraper.Services.HighLowPredictionsService.getHighLowTidePredictionsRequest request);
        
        // CODEGEN: Generating message contract since the operation getHLPredAndMetadata is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        TideScraper.Services.HighLowPredictionsService.getHLPredAndMetadataResponse getHLPredAndMetadata(TideScraper.Services.HighLowPredictionsService.getHLPredAndMetadataRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl")]
    public partial class Parameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string stationIdField;
        
        private string beginDateField;
        
        private string endDateField;
        
        private int datumField;
        
        private int unitField;
        
        private int timeZoneField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string stationId {
            get {
                return this.stationIdField;
            }
            set {
                this.stationIdField = value;
                this.RaisePropertyChanged("stationId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string beginDate {
            get {
                return this.beginDateField;
            }
            set {
                this.beginDateField = value;
                this.RaisePropertyChanged("beginDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string endDate {
            get {
                return this.endDateField;
            }
            set {
                this.endDateField = value;
                this.RaisePropertyChanged("endDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public int datum {
            get {
                return this.datumField;
            }
            set {
                this.datumField = value;
                this.RaisePropertyChanged("datum");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public int unit {
            get {
                return this.unitField;
            }
            set {
                this.unitField = value;
                this.RaisePropertyChanged("unit");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public int timeZone {
            get {
                return this.timeZoneField;
            }
            set {
                this.timeZoneField = value;
                this.RaisePropertyChanged("timeZone");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl")]
    public partial class highlowtidepred : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string timeField;
        
        private double predField;
        
        private string typeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string time {
            get {
                return this.timeField;
            }
            set {
                this.timeField = value;
                this.RaisePropertyChanged("time");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public double pred {
            get {
                return this.predField;
            }
            set {
                this.predField = value;
                this.RaisePropertyChanged("pred");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
                this.RaisePropertyChanged("type");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl")]
    public partial class Data : object, System.ComponentModel.INotifyPropertyChanged {
        
        private highlowtidepred[] dataField;
        
        private string dateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("data", Order=0)]
        public highlowtidepred[] data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
                this.RaisePropertyChanged("data");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string date {
            get {
                return this.dateField;
            }
            set {
                this.dateField = value;
                this.RaisePropertyChanged("date");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl")]
    public partial class HighLowValues : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Data[] highLowValues1Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute("HighLowValues", IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable=false)]
        public Data[] HighLowValues1 {
            get {
                return this.highLowValues1Field;
            }
            set {
                this.highLowValues1Field = value;
                this.RaisePropertyChanged("HighLowValues1");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getHighLowTidePredictionsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl", Order=0)]
        public TideScraper.Services.HighLowPredictionsService.Parameters Parameters;
        
        public getHighLowTidePredictionsRequest() {
        }
        
        public getHighLowTidePredictionsRequest(TideScraper.Services.HighLowPredictionsService.Parameters Parameters) {
            this.Parameters = Parameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getHighLowTidePredictionsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl", Order=0)]
        public TideScraper.Services.HighLowPredictionsService.HighLowValues HighLowValues;
        
        public getHighLowTidePredictionsResponse() {
        }
        
        public getHighLowTidePredictionsResponse(TideScraper.Services.HighLowPredictionsService.HighLowValues HighLowValues) {
            this.HighLowValues = HighLowValues;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl")]
    public partial class HighLowAndMetadata : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string stationIdField;
        
        private string stationNameField;
        
        private float latitudeField;
        
        private float longitudeField;
        
        private string stateField;
        
        private string dataSourceField;
        
        private string cOOPSDisclaimerField;
        
        private string beginDateField;
        
        private string endDateField;
        
        private string datumField;
        
        private string unitField;
        
        private string timeZoneField;
        
        private Data[] highLowValuesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string stationId {
            get {
                return this.stationIdField;
            }
            set {
                this.stationIdField = value;
                this.RaisePropertyChanged("stationId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string stationName {
            get {
                return this.stationNameField;
            }
            set {
                this.stationNameField = value;
                this.RaisePropertyChanged("stationName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public float latitude {
            get {
                return this.latitudeField;
            }
            set {
                this.latitudeField = value;
                this.RaisePropertyChanged("latitude");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public float longitude {
            get {
                return this.longitudeField;
            }
            set {
                this.longitudeField = value;
                this.RaisePropertyChanged("longitude");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string state {
            get {
                return this.stateField;
            }
            set {
                this.stateField = value;
                this.RaisePropertyChanged("state");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string dataSource {
            get {
                return this.dataSourceField;
            }
            set {
                this.dataSourceField = value;
                this.RaisePropertyChanged("dataSource");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string COOPSDisclaimer {
            get {
                return this.cOOPSDisclaimerField;
            }
            set {
                this.cOOPSDisclaimerField = value;
                this.RaisePropertyChanged("COOPSDisclaimer");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string beginDate {
            get {
                return this.beginDateField;
            }
            set {
                this.beginDateField = value;
                this.RaisePropertyChanged("beginDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string endDate {
            get {
                return this.endDateField;
            }
            set {
                this.endDateField = value;
                this.RaisePropertyChanged("endDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string datum {
            get {
                return this.datumField;
            }
            set {
                this.datumField = value;
                this.RaisePropertyChanged("datum");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string unit {
            get {
                return this.unitField;
            }
            set {
                this.unitField = value;
                this.RaisePropertyChanged("unit");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string timeZone {
            get {
                return this.timeZoneField;
            }
            set {
                this.timeZoneField = value;
                this.RaisePropertyChanged("timeZone");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=12)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable=false)]
        public Data[] HighLowValues {
            get {
                return this.highLowValuesField;
            }
            set {
                this.highLowValuesField = value;
                this.RaisePropertyChanged("HighLowValues");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getHLPredAndMetadataRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl", Order=0)]
        public TideScraper.Services.HighLowPredictionsService.Parameters Parameters;
        
        public getHLPredAndMetadataRequest() {
        }
        
        public getHLPredAndMetadataRequest(TideScraper.Services.HighLowPredictionsService.Parameters Parameters) {
            this.Parameters = Parameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getHLPredAndMetadataResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://opendap.co-ops.nos.noaa.gov/axis/webservices/highlowtidepred/wsdl", Order=0)]
        public TideScraper.Services.HighLowPredictionsService.HighLowAndMetadata HighLowAndMetadata;
        
        public getHLPredAndMetadataResponse() {
        }
        
        public getHLPredAndMetadataResponse(TideScraper.Services.HighLowPredictionsService.HighLowAndMetadata HighLowAndMetadata) {
            this.HighLowAndMetadata = HighLowAndMetadata;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface highlowtidepredPortTypeChannel : TideScraper.Services.HighLowPredictionsService.highlowtidepredPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class highlowtidepredPortTypeClient : System.ServiceModel.ClientBase<TideScraper.Services.HighLowPredictionsService.highlowtidepredPortType>, TideScraper.Services.HighLowPredictionsService.highlowtidepredPortType {
        
        public highlowtidepredPortTypeClient() {
        }
        
        public highlowtidepredPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public highlowtidepredPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public highlowtidepredPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public highlowtidepredPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TideScraper.Services.HighLowPredictionsService.getHighLowTidePredictionsResponse TideScraper.Services.HighLowPredictionsService.highlowtidepredPortType.getHighLowTidePredictions(TideScraper.Services.HighLowPredictionsService.getHighLowTidePredictionsRequest request) {
            return base.Channel.getHighLowTidePredictions(request);
        }
        
        public TideScraper.Services.HighLowPredictionsService.HighLowValues getHighLowTidePredictions(TideScraper.Services.HighLowPredictionsService.Parameters Parameters) {
            TideScraper.Services.HighLowPredictionsService.getHighLowTidePredictionsRequest inValue = new TideScraper.Services.HighLowPredictionsService.getHighLowTidePredictionsRequest();
            inValue.Parameters = Parameters;
            TideScraper.Services.HighLowPredictionsService.getHighLowTidePredictionsResponse retVal = ((TideScraper.Services.HighLowPredictionsService.highlowtidepredPortType)(this)).getHighLowTidePredictions(inValue);
            return retVal.HighLowValues;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TideScraper.Services.HighLowPredictionsService.getHLPredAndMetadataResponse TideScraper.Services.HighLowPredictionsService.highlowtidepredPortType.getHLPredAndMetadata(TideScraper.Services.HighLowPredictionsService.getHLPredAndMetadataRequest request) {
            return base.Channel.getHLPredAndMetadata(request);
        }
        
        public TideScraper.Services.HighLowPredictionsService.HighLowAndMetadata getHLPredAndMetadata(TideScraper.Services.HighLowPredictionsService.Parameters Parameters) {
            TideScraper.Services.HighLowPredictionsService.getHLPredAndMetadataRequest inValue = new TideScraper.Services.HighLowPredictionsService.getHLPredAndMetadataRequest();
            inValue.Parameters = Parameters;
            TideScraper.Services.HighLowPredictionsService.getHLPredAndMetadataResponse retVal = ((TideScraper.Services.HighLowPredictionsService.highlowtidepredPortType)(this)).getHLPredAndMetadata(inValue);
            return retVal.HighLowAndMetadata;
        }
    }
}
