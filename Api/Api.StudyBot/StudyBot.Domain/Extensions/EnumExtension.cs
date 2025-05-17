using System.ComponentModel;
using System.Reflection;

namespace StudyBot.Domain.Extensions
{
    public static class EnumExtension
    {
        public static bool HasValue(this Enum value)
        {
            return value != null
                && Enum.IsDefined(value?.GetType(), value);
        }

        public static string GetDescription(this Enum value)
        {
            var hasValue = HasValue(value);

            if (hasValue)
            {
                FieldInfo? field = value.GetType().GetField(value.ToString());

                var attribute = Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute)) as DescriptionAttribute;

                return attribute == null ? value.ToString() : attribute.Description;
            }
            return string.Empty;
        }

        public static T GetValueFromDescription<T>(string description)
            where T : Enum
        {
            try
            {
                foreach (var field in typeof(T).GetFields())
                {
                    if (
                        Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        is DescriptionAttribute attribute
                    )
                    {
                        if (attribute.Description == description)
                            return (T)field.GetValue(null);
                    }
                    else
                    {
                        if (field.Name == description)
                            return (T)field.GetValue(null);
                    }
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Not found.", nameof(description));
            }

            return default(T);
        }

        public static T GetValueFromEnumerator<T>(int enumerator)
            where T : Enum
        {
            string enumeratorName = Enum.GetName(typeof(T), enumerator);

            if (string.IsNullOrEmpty(enumeratorName))
            {
                return default(T);
            }

            return (T)Enum.Parse(typeof(T), enumeratorName);
        }


        public static string GetEnumDescription<T>(string value)
            where T : Enum
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            try
            {
                T enumType = (T)Enum.Parse(typeof(T), value);

                return enumType.GetDescription();
            }
            catch
            {
                return string.Empty;
            }
        }
    }

}