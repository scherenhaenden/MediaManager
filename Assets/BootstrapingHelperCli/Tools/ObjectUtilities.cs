namespace BootstrapingHelperCli.Tools;

public class ObjectUtilities
{
    
    public static T CopyObject<T>(T source)
    {
        var targetType = typeof(T);
        var copiedObject = Activator.CreateInstance(targetType);

        foreach (var propertyInfo in targetType.GetProperties())
        {
            if (propertyInfo.CanRead && propertyInfo.CanWrite)
            {
                var value = propertyInfo.GetValue(source);
                propertyInfo.SetValue(copiedObject, value);
            }
        }

        return (T)copiedObject;
    }
}