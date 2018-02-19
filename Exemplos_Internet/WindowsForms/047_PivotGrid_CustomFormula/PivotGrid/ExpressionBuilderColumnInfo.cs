using System;
using DevExpress.Data;
using System.Collections.Generic;
using DevExpress.Data.Filtering;

namespace DXSample {
    public class ExpressionBuilderColumnInfo :IDataColumnInfo {
        private List<IDataColumnInfo> fColumns;
        private string fExpression;

        public ExpressionBuilderColumnInfo(List<IDataColumnInfo> columns, string expression) {
            fColumns = columns;
            fExpression = expression;
        }

        #region IDataColumnInfo Members

        string IDataColumnInfo.Caption {
            get { return "Custom Summary Expression"; }
        }

        List<IDataColumnInfo> IDataColumnInfo.Columns {
            get { return fColumns; }
        }

        DataControllerBase IDataColumnInfo.Controller {
            get { return null; }
        }

        string IDataColumnInfo.FieldName {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        Type IDataColumnInfo.FieldType {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        string IDataColumnInfo.Name {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        string IDataColumnInfo.UnboundExpression {
            get { return fExpression; }
        }

        #endregion
    }

    public struct CustomSummaryExpressionInfo {
        public CustomSummaryExpressionInfo(string _expression, Aggregate _aggregate) {
            Expression = _expression;
            Aggregate = _aggregate;
        }

        public string Expression;
        public Aggregate Aggregate;

        public override string ToString() {
            return string.Concat(Expression, ";", Aggregate);
        }
    }

    public class NumericObject {
        private NumericType fType;
        private int iVal;
        private float fVal;
        private double dbVal;
        private decimal dcVal;

        public NumericObject(object source) {
            if (source == null) throw new ArgumentNullException("source");
            Type sourceType = source.GetType();
            if (sourceType == typeof(int)) {
                iVal = (int)source;
                fType = NumericType.Integer;
            } else if (sourceType == typeof(float)) {
                fVal = (float)source;
                fType = NumericType.Float;
            } else if (sourceType == typeof(double)) {
                dbVal = (double)source;
                fType = NumericType.Double;
            } else if (sourceType == typeof(decimal)) {
                dcVal = (decimal)source;
                fType = NumericType.Decimal;
            } else if (sourceType == typeof(NumericObject)) {
                NumericObject sObj = (NumericObject)source;
                fType = sObj.fType;
                iVal = sObj.iVal;
                fVal = sObj.fVal;
                dbVal = sObj.dbVal;
                dcVal = sObj.dcVal;
            } else throw new NotSupportedException(); 
        }

        public object Value {
            get {
                switch (fType) {
                    case NumericType.Decimal: return dcVal;
                    case NumericType.Double: return dbVal;
                    case NumericType.Float: return fVal;
                    case NumericType.Integer: return iVal;
                }
                throw new NotSupportedException();
            }
        }

        public static NumericObject operator +(NumericObject left, NumericObject right) {
            if (left.fType != right.fType) throw new NotSupportedException();
            switch (left.fType) {
                case NumericType.Decimal: return new NumericObject(left.dcVal + right.dcVal);
                case NumericType.Double: return new NumericObject(left.dbVal + right.dbVal);
                case NumericType.Float: return new NumericObject(left.fVal + right.fVal);
                case NumericType.Integer: return new NumericObject(left.iVal + right.iVal);
            }
            throw new NotSupportedException();
        }

        public static NumericObject operator /(NumericObject left, NumericObject right) {
            if (left.fType != right.fType) {
                if (right.fType != NumericType.Integer) {
                    int rVal = right.iVal;
                    switch (left.fType) {
                        case NumericType.Decimal: return new NumericObject(left.dcVal / rVal);
                        case NumericType.Double: return new NumericObject(left.dbVal / rVal);
                        case NumericType.Float: return new NumericObject(left.fVal / rVal);
                        case NumericType.Integer: return new NumericObject(left.iVal / rVal);
                    }
                }
            } else {
                switch (left.fType) {
                    case NumericType.Decimal: return new NumericObject(left.dcVal / right.dcVal);
                    case NumericType.Double: return new NumericObject(left.dbVal / right.dbVal);
                    case NumericType.Float: return new NumericObject(left.fVal / right.fVal);
                    case NumericType.Integer: return new NumericObject(left.iVal / right.iVal);
                }
            }
            throw new NotSupportedException();
        }

        public static NumericObject Max(NumericObject left, NumericObject right) {
            if (left.fType != right.fType) throw new NotSupportedException();
            switch (left.fType) {
                case NumericType.Decimal: return new NumericObject(Math.Max(left.dcVal, right.dcVal));
                case NumericType.Double: return new NumericObject(Math.Max(left.dbVal, right.dbVal));
                case NumericType.Float: return new NumericObject(Math.Max(left.fVal, right.fVal));
                case NumericType.Integer: return new NumericObject(Math.Max(left.iVal, right.iVal));
            }
            throw new NotSupportedException();
        }

        public static NumericObject Min(NumericObject left, NumericObject right) {
            if (left.fType != right.fType) throw new NotSupportedException();
            switch (left.fType) {
                case NumericType.Decimal: return new NumericObject(Math.Min(left.dcVal, right.dcVal));
                case NumericType.Double: return new NumericObject(Math.Min(left.dbVal, right.dbVal));
                case NumericType.Float: return new NumericObject(Math.Min(left.fVal, right.fVal));
                case NumericType.Integer: return new NumericObject(Math.Min(left.iVal, right.iVal));
            }
            throw new NotSupportedException();
        }

        private enum NumericType { Integer, Float, Double, Decimal }
    }
}
