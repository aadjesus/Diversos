// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Espresso {

    public abstract class Dynamo {
        public Dynamo() {
        }
        public override string ToString() {
            FieldInfo[] fis = this.GetType().GetFields(BindingFlags.Instance|BindingFlags.Public);
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            for (int i = 0, n = fis.Length; i < n; i++) {
                if (i > 0) sb.Append(", ");                    
                sb.Append(fis[i].Name);
                sb.Append("=");
                sb.Append(fis[i].GetValue(this));
            }
            sb.Append("}");     
            return sb.ToString();       
        }
    }
    
    public struct DynamoField : IEquatable<DynamoField> {
        string name;
        Type fieldType;
        public DynamoField(string name, Type fieldType) {
            this.name = name;
            this.fieldType = fieldType;
        }
        public string Name {
            get { return this.name; }
        }
        public Type FieldType {
            get { return this.fieldType; }
        }
        public bool Equals(DynamoField f) {
            return string.Compare(name, f.Name) == 0 && fieldType == f.fieldType;
        }        
        public override int GetHashCode() {
            return name.GetHashCode() ^ fieldType.GetHashCode();
        }
        public override bool Equals(object obj) {
            if (!(obj is DynamoField)) 
                return false;
            return this.Equals((DynamoField)obj);
        }
    }
    
    public sealed class DynamoFactory {
        ModuleBuilder module;      
        int nTypes;
        Dictionary<DynamoDecl, Type> dynamos;
        
        public DynamoFactory() {
            AssemblyName name = new AssemblyName("DynamoAssembly" + this.GetHashCode());
            AssemblyBuilder ass = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
            this.module = ass.DefineDynamicModule("DynamoModule");
            this.dynamos = new Dictionary<DynamoDecl, Type>();
        }
        
        public Type GetDynamo(DynamoField[] fields) {
            DynamoDecl decl = new DynamoDecl(fields);
            Type type;
            if (!this.dynamos.TryGetValue(decl, out type)) {
                type = BuildDynamo(fields);
                this.dynamos.Add(decl, type);
            }
            return type;
        }
        
        private Type BuildDynamo(DynamoField[] fields) {
            string typeName = "Dynamo" + (nTypes++);
            TypeBuilder tb = this.module.DefineType(typeName, TypeAttributes.Class, typeof(Dynamo));
            for (int i = 0, n = fields.Length; i < n; i++) {
                tb.DefineField(fields[i].Name, fields[i].FieldType, FieldAttributes.Public);
            }
            Type type = tb.CreateType();
            return type;
        }
        
        internal struct DynamoDecl : IEquatable<DynamoDecl> {
            DynamoField[] fields;
            int hashcode;
            internal DynamoDecl(DynamoField[] fields) {
                this.fields = fields;                
                this.hashcode = 0;
                for (int i = 0, n = fields.Length; i < n; i++) {
                    this.hashcode ^= fields[i].GetHashCode();
                }
            }
            public bool Equals(DynamoDecl decl) {
                int n = this.fields.Length;
                if (n != decl.fields.Length)
                    return false;
                for (int i = 0; i < n; i++) {
                    if (!this.fields[i].Equals(decl.fields[i]))
                        return false;
                }
                return true;
            }            
            public override int GetHashCode() {
                return this.hashcode;
            }
            public override bool Equals(object obj) {
                if (!(obj is DynamoDecl))
                    return false;
                return this.Equals((DynamoDecl)obj);
            }
        }               
    }
}
