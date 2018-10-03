using System;
using System.Reflection;

namespace Controls
{
    public class Reflection
    {
        public static bool GetValue(object Obj, string PropertyPath, out object Value, out object ValueParent)
        {
            object Current = Obj;
            object Parent = null;
            string[] FieldNames = PropertyPath.Split('.');



            foreach (string FieldName in FieldNames)
            {
                Type CurrentType = Current.GetType();
                PropertyInfo Property = CurrentType.GetProperty(FieldName);

                if (Property != null)
                {
                    Parent = Current;
                    Current = Property.GetValue(Current, null);

                    if (Current == null)
                    {
                        Value = null;
                        ValueParent = null;
                        return false;
                    }
                }
                else
                {
                    Value = null;
                    ValueParent = null;
                    return false;
                }
            }
            Value = Current;
            ValueParent = Parent;
            return true;
        }
    }
}
