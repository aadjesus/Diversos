﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCF_Client_SvcUtil.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SvcUtilPath {
            get {
                return ((string)(this["SvcUtilPath"]));
            }
            set {
                this["SvcUtilPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SvcUtil.exe")]
        public string SvcUtilFilename {
            get {
                return ((string)(this["SvcUtilFilename"]));
            }
            set {
                this["SvcUtilFilename"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ParamAsync {
            get {
                return ((bool)(this["ParamAsync"]));
            }
            set {
                this["ParamAsync"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ParamMergeConfig {
            get {
                return ((bool)(this["ParamMergeConfig"]));
            }
            set {
                this["ParamMergeConfig"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ParamSerializable {
            get {
                return ((bool)(this["ParamSerializable"]));
            }
            set {
                this["ParamSerializable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TxtOutputDirectory_Text {
            get {
                return ((string)(this["TxtOutputDirectory_Text"]));
            }
            set {
                this["TxtOutputDirectory_Text"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("GeneratedProxy.cs")]
        public string TxtProxyFilename_Text {
            get {
                return ((string)(this["TxtProxyFilename_Text"]));
            }
            set {
                this["TxtProxyFilename_Text"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("app.config")]
        public string TxtConfigFilename_Text {
            get {
                return ((string)(this["TxtConfigFilename_Text"]));
            }
            set {
                this["TxtConfigFilename_Text"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://")]
        public string TxtServiceAddress_Text {
            get {
                return ((string)(this["TxtServiceAddress_Text"]));
            }
            set {
                this["TxtServiceAddress_Text"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool StartWithAereoGlass {
            get {
                return ((bool)(this["StartWithAereoGlass"]));
            }
            set {
                this["StartWithAereoGlass"] = value;
            }
        }
    }
}