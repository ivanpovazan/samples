
using System.Reflection;
using System.Collections.Generic;

namespace HelloMauiAndroid;

public static class Utils
{
	public static void DumpDynamicallyInvokedMethodStats()
	{
		Console.WriteLine($" --> RuntimeFeature.IsDynamicCodeCompiled: {System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeCompiled}");

		object invokedMethodsObj;
		string typeName = "System.Reflection.IP_Diagnostics";
		string methodName = "GetInvokedMethods";

		Type type = typeof(MethodBase).Assembly.GetType(typeName);
		if (type == null)
			Console.WriteLine($"--->>> [FATAL] Type: {typeName} failed to load");
		
		MethodInfo methodInfo = type.GetMethod(methodName);
		if (methodInfo == null)
			Console.WriteLine($"--->>> [FATAL] Method: {methodName} failed to load");

		invokedMethodsObj = methodInfo.Invoke(null, null); // invoking static method
		if (invokedMethodsObj == null)
			Console.WriteLine($"--->>> [FATAL] Method failed to execute");

		IDictionary<MethodBase, IList<(bool, string)>> invokedMethodsDict = (IDictionary<MethodBase, IList<(bool, string)>>)invokedMethodsObj;
		foreach (var invokedMethod in invokedMethodsDict)
		{
			MethodBase mb = invokedMethod.Key;
			string currMethodName = $"{mb.ReflectedType.Name}.{mb.Name}";
			Console.WriteLine($"--->>> Method: [{currMethodName}] called:{invokedMethod.Value.Count} times");
			int invokedInterpreted = 0;
			int invokedThroughDelegate = 0;
			foreach (var pair in invokedMethod.Value)
			{
				Console.WriteLine($" '{currMethodName}'[{pair.Item1}]: {pair.Item2}");
				if (pair.Item1)
					invokedThroughDelegate++;
				else
					invokedInterpreted++;
			}
			Console.WriteLine($"<<<--- Method: [{currMethodName}] called:{invokedMethod.Value.Count} times [interp:{invokedInterpreted},deleg:{invokedThroughDelegate}]");
		}
	}
}
