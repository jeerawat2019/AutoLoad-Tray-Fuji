using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ai_PCSystem.Strings
{
    public static class Str_Attributes
    {
        //typeof(Group).GetProperty("UserExistsInGroup");
        public static String GetEnumDescription(this Enum obj)
        {
            try
            {
                System.Reflection.FieldInfo fieldInfo =
                    obj.GetType().GetField(obj.ToString());

                object[] attribArray = fieldInfo.GetCustomAttributes(false);

                if (attribArray.Length > 0)
                {
                    var attrib = attribArray[0] as DescriptionAttribute;

                    if (attrib != null)
                        return attrib.Description;
                }
                return obj.ToString();
            }
            catch (NullReferenceException ex)
            {
                return "Unknown";
            }
        }

        //class MyClass
        //{
        //    public string Name { get; set; }
        //    [Description("The age description")]
        //    public int Age { get; set; }
        //}
        //string ageDescription = GetDescription<MyClass>("Age");
        //console.log(ageDescription) // OUTPUT: The age description
        //tring ageDescription = GetDescription<MyClass>("Age");
        public static string GetDescription_s3<T>(string fieldName)
        {
            string result;
            //FieldInfo fi = typeof(T).GetField(fieldName.ToString());
            PropertyInfo pr = typeof(T).GetProperty(fieldName.ToString());
            if (pr != null)
            {
                try
                {
                    object[] descriptionAttrs = pr.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    DescriptionAttribute description = (DescriptionAttribute)descriptionAttrs[0];                    
                    result = (description.Description);
                }
                catch
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }

            return result;
        }
        public static string GetCategory_s<T>(string fieldName)
        {
            string result;
            //FieldInfo fi = typeof(T).GetField(fieldName.ToString());
            PropertyInfo pr = typeof(T).GetProperty(fieldName.ToString());
            if (pr != null) {
                try {
                    object[] descriptionAttrs = pr.GetCustomAttributes(typeof(CategoryAttribute), false);
                    CategoryAttribute description = (CategoryAttribute)descriptionAttrs[0];
                    result = (description.Category);
                }
                catch {
                    result = null;
                }
            }
            else {
                result = null;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static void attributecheck<T>()
        {
            var props = typeof(T).GetProperties();
            foreach (var propertyInfo in props)
            {
                var att = propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (att.Length > 0)
                {
                }
            }
        }
        //Type type = input.GetType();
        //MemberInfo[] memInfo = type.GetMember(input.ToString());
        public static string GetDescription(System.Enum input)
        {
            Type type = input.GetType();
            MemberInfo[] memInfo = type.GetMember(input.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = (object[])memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return input.ToString();
        }
        //public enum MyEnum
        //{
        //    Name1 = 1,
        //    [Description("Here is another")]
        //    HereIsAnother = 2,
        //    [Description("Last one")]
        //    LastOne = 3
        //}
        public static string GetEnumDescriptions(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static string GetDescriptionfromValue(this Enum value)
        {
            var enumType = value.GetType();
            var field = enumType.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }
        public static string GetDescription<T>(string fieldName)
        {
            string result;
            FieldInfo fi = typeof(T).GetField(fieldName.ToString());
            if (fi != null) {
                try {
                    object[] descriptionAttrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    DescriptionAttribute description = (DescriptionAttribute)descriptionAttrs[0];
                    result = (description.Description);
                }
                catch {
                    result = null;
                }
            }
            else {
                result = null;
            }

            return result;
        }
        //static void Main()
        //{
        //    PropertyInfo prop = typeof(Foo).GetProperty("Bar");
        //    var vals = GetPropertyAttributes(prop);
        //    // has: DisplayName = "abc", Browsable = false
        //}
        public static Dictionary<string, object> GetPropertyAttributes(PropertyInfo property)
        {
            Dictionary<string, object> attribs = new Dictionary<string, object>();
            // look for attributes that takes one constructor argument
            foreach (CustomAttributeData attribData in property.GetCustomAttributesData())
            {

                if (attribData.ConstructorArguments.Count == 1)
                {
                    string typeName = attribData.Constructor.DeclaringType.Name;
                    if (typeName.EndsWith("Attribute")) typeName = typeName.Substring(0, typeName.Length - 9);
                    attribs[typeName] = attribData.ConstructorArguments[0].Value;
                }

            }
            return attribs;
        }
        public static string GetDescription_s(System.Enum input)
        {
            Type type = input.GetType();
            MemberInfo[] memInfo = type.GetMember(input.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = (object[])memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return input.ToString();
        }
        //public static string GetDescription_s1(System.Enum value)
        //{
        //    var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        //    var descriptionAttribute =
        //        enumMember == null
        //            ? default(DescriptionAttribute)
        //            : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
        //    return
        //        descriptionAttribute == null
        //            ? value.ToString()
        //            : descriptionAttribute.Description;
        //}
        //public static string GetDescription_s2(Enum value)
        //{
        //    return
        //        value
        //            .GetType()
        //            .GetMember(value.ToString())
        //            .FirstOrDefault()
        //            ?.GetCustomAttribute<DescriptionAttribute>()
        //            ?.Description
        //        ?? value.ToString();
        //}
        public static void SetValue(object inputObject, string propertyName, object propertyVal)
        {
            //find out the type
            Type type = inputObject.GetType();

            //get the property information based on the type
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);

            //find the property type
            Type propertyType = propertyInfo.PropertyType;

            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

            //Returns an System.Object with the specified System.Type and whose value is
            //equivalent to the specified object.
            propertyVal = Convert.ChangeType(propertyVal, targetType);

            //Set the value of the property
            propertyInfo.SetValue(inputObject, propertyVal, null);

        }

        //class Person
        //{
        //    public int Name { get; set; }
        //    public bool BoolVar { get; set; }
        //    public float FloatVar { get; set; }
        //    public DateTime DateVar { get; set; }
        //}
        //class MainClass
        //{
        //    public static void Main(string[] args)
        //    {
        //        Person p = new Person();
        //        SetPropertyValue(p, "BoolVar", true);
        //        SetPropertyValue(p, "FloatVar", 11.5);
        //        SetPropertyValue(p, "DateVar", "2015-05-01");
        //        Console.WriteLine(p.BoolVar);
        //        Console.WriteLine(p.FloatVar);
        //        Console.WriteLine(p.DateVar);
        //    }
        //}

        //Ai_PCSystem.Strings.Str_Attributes.SetPropertyValue(resultAll, propertyInfos[0].Name, new Dictionary<string, object>()
        //{
        //    { "Part",new Part()
        //        {   ID = "001",
        //            Name = "LamottaMax",
        //            Desc = "",
        //            Date = DateTime.Now.ToString("MM/dd/yyyy"),
        //            Time = DateTime.Now.ToString("H:mm"),
        //            PDprocess = ProductionProcess.LaserMark, }
        //    },
        //    {"Data", "XXX-XX-XXXXX-X" },
        //    {"DateTime",DateTime.Now.ToLongTimeString() },

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }
        public static IDictionary<string, object> GetDictionary(object o)
        {
            IDictionary<string, object> res = new Dictionary<string, object>();
            PropertyInfo[] props = typeof(object).GetProperties();
            for (int i = 0; i < props.Length; i++) {
                if (props[i].CanRead) {
                    res.Add(props[i].Name, props[i].GetValue(o, null));
                }
            }
            return res;
        }
        public static void SetPropertyValue(object obj, string propertyName, object propertyValue)
        {
            if (obj == null || string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            Type objectType = obj.GetType();

            PropertyInfo propertyDetail = objectType.GetProperty(propertyName);


            if (propertyDetail != null && propertyDetail.CanWrite)
            {
                Type propertyType = propertyDetail.PropertyType;

                Type dataType = propertyType;

                // Check for nullable types
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // Check for null or empty string value.
                    if (propertyValue == null || string.IsNullOrWhiteSpace(propertyValue.ToString()))
                    {

                        propertyDetail.SetValue(obj, null, null);
                        return;
                    }
                    else
                    {
                        dataType = propertyType.GetGenericArguments()[0];
                    }
                }

                propertyValue = Convert.ChangeType(propertyValue, propertyType);

                propertyDetail.SetValue(obj, propertyValue, null);

            }
        }
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static Object GetPropValues(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValues<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }
        public static object GetObjProperty(object obj, string property)
        {
            Type t = obj.GetType();
            PropertyInfo p = t.GetProperty("Location");
            Point location = (Point)p.GetValue(obj, null);
            return location;
        }
        public static List<KeyValuePair<string, string>> GetProperties(object item) //where T : class
        {
            var result = new List<KeyValuePair<string, string>>();
            if (item != null)
            {
                var type = item.GetType();
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var pi in properties)
                {
                    var selfValue = type.GetProperty(pi.Name).GetValue(item, null);
                    if (selfValue != null)
                    {
                        result.Add(new KeyValuePair<string, string>(pi.Name, selfValue.ToString()));
                    }
                    else
                    {
                        result.Add(new KeyValuePair<string, string>(pi.Name, null));
                    }
                }
            }
            return result;
        }
        public static object GetPropertyValue(object srcobj, string propertyName)
        {
            if (srcobj == null)
                return null;

            object obj = srcobj;

            // Split property name to parts (propertyName could be hierarchical, like obj.subobj.subobj.property
            string[] propertyNameParts = propertyName.Split('.');

            foreach (string propertyNamePart in propertyNameParts)
            {
                if (obj == null) return null;

                // propertyNamePart could contain reference to specific 
                // element (by index) inside a collection
                if (!propertyNamePart.Contains("["))
                {
                    PropertyInfo pi = obj.GetType().GetProperty(propertyNamePart);
                    if (pi == null) return null;
                    obj = pi.GetValue(obj, null);
                }
                else
                {   // propertyNamePart is areference to specific element 
                    // (by index) inside a collection
                    // like AggregatedCollection[123]
                    //   get collection name and element index
                    int indexStart = propertyNamePart.IndexOf("[") + 1;
                    string collectionPropertyName = propertyNamePart.Substring(0, indexStart - 1);
                    int collectionElementIndex = Int32.Parse(propertyNamePart.Substring(indexStart, propertyNamePart.Length - indexStart - 1));
                    //   get collection object
                    PropertyInfo pi = obj.GetType().GetProperty(collectionPropertyName);
                    if (pi == null) return null;
                    object unknownCollection = pi.GetValue(obj, null);
                    //   try to process the collection as array
                    if (unknownCollection.GetType().IsArray)
                    {
                        object[] collectionAsArray = unknownCollection as object[];
                        obj = collectionAsArray[collectionElementIndex];
                    }
                    else
                    {
                        //   try to process the collection as IList
                        System.Collections.IList collectionAsList = unknownCollection as System.Collections.IList;
                        if (collectionAsList != null)
                        {
                            obj = collectionAsList[collectionElementIndex];
                        }
                        else
                        {
                            // ??? Unsupported collection type
                        }
                    }
                }
            }

            return obj;
        }
        //class Foo
        //{
        //    [DisplayName("abc")]
        //    [Browsable(false)]
        //    public string Bar { get; set; }
        //}
    }
    [Developer("Joan Smith", "42", Reviewed = true)]
    class MainApp
    {
        public static void Main()
        {
            // Call function to get and display the attribute.
            GetAttribute(typeof(MainApp));
        }

        public static void GetAttribute(Type t)
        {
            // Get instance of the attribute.
            Developer MyAttribute =
                (Developer)Attribute.GetCustomAttribute(t, typeof(Developer));

            if (MyAttribute == null)
            {
                Console.WriteLine("The attribute was not found.");
            }
            else
            {
                // Get the Name value.
                Console.WriteLine("The Name Attribute is: {0}.", MyAttribute.Name);
                // Get the Level value.
                Console.WriteLine("The Level Attribute is: {0}.", MyAttribute.Level);
                // Get the Reviewed value.
                Console.WriteLine("The Reviewed Attribute is: {0}.", MyAttribute.Reviewed);
            }
        }
    }
}
