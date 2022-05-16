using System.Linq;
using System.Text;

namespace QKNWZ1.WpfApp
{
    public static class ExtensionClass
    {
        /// <summary>
        /// Returns a <see cref="string"/> representation of the properties of <typeparamref name="T"/> that are decorated with the <see cref="PropToStringAttribute"/>.
        /// </summary>
        /// <typeparam name="T">Any type that is a class.</typeparam>
        /// <param name="instance">The instance of <typeparamref name="T"/> that will be used to read the current property values.</param>
        /// <param name="isEnough">Whether the closing brace <c>}</c> be appended at the end.</param>
        /// <returns>The <see cref="string"/> representation of decorated properties in <paramref name="instance"/>.</returns>
        public static string PropsToString<T>(this T instance, bool isEnough = true) where T : class
        {
            var tType = typeof(T);
            StringBuilder sb = new StringBuilder(tType.Name)
                .Append(" {");

            var propertyInfos = tType.GetProperties()
                .Where(propInfo => propInfo.GetCustomAttributes(typeof(PropToStringAttribute), false).Length > 0);

            string[] values = (string[])System.Array.CreateInstance(typeof(string), propertyInfos.Count());

            for (int i = 0; i < values.Length; i++)
            {
                var propInfo = propertyInfos.ElementAt(i);
                values[i] = $" {propInfo.Name} = {propInfo.GetValue(instance)} ";
            }
            for (int i = 0; i < values.Length - 1; i++)
            {
                sb.Append(values[i]).Append("|");
            }

            sb.Append(values[^1])
                .Append(isEnough ? " }" : string.Empty);
            return sb.ToString();
        }
    }
}
