using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace N8Engine
{
    internal static class AttributesHelper
    {
        public const BindingFlags STATIC = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        public const BindingFlags NON_STATIC = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private static IEnumerable<Type> _allTypes;
        private static IEnumerable<Type> AllTypes
        {
            get
            {
                if (_allTypes is null)
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    var types = new List<Type>();
                    foreach (var assembly in assemblies)
                    {
                        var assemblyTypes = assembly.GetTypes();
                        foreach (var assemblyType in assemblyTypes)
                            types.Add(assemblyType);
                    }
                    _allTypes = types;
                }
                return _allTypes;
            }
        }
        
        public static IEnumerable<MemberInfo> FindVariables(BindingFlags flags)
        {
            var variables = new List<MemberInfo>();
            foreach (var type in AllTypes)
            {
                var typeVariables = type.GetMembers(flags);
                foreach (var typeVariable in typeVariables)
                    variables.Add(typeVariable);
            }
            return variables;
        }
        
        public static IEnumerable<MemberInfo> WithCustomAttribute<T>(this IEnumerable<MemberInfo> variables) where T : Attribute
        {
            var variablesWithCustomAttribute = new List<MemberInfo>();
            foreach (var variable in variables)
                if (variable.CustomAttributes.Any())
                {
                    var attribute = variable.GetCustomAttribute<T>();
                    if (attribute is not null)
                        variablesWithCustomAttribute.Add(variable);
                }
            return variablesWithCustomAttribute;
        }
        
        public static IEnumerable<MethodInfo> FindMethods(BindingFlags flags)
        {
            var methods = new List<MethodInfo>();
            foreach (var type in AllTypes)
            {
                var typeMethods = type.GetMethods(flags);
                foreach (var typeMethod in typeMethods)
                    methods.Add(typeMethod);
            }
            return methods;
        }

        public static IEnumerable<MethodInfo> WithCustomAttribute<T>(this IEnumerable<MethodInfo> methods) where T : Attribute
        {
            var methodsWithCustomAttribute = new List<MethodInfo>();
            foreach (var method in methods)
                if (method.CustomAttributes.Any())
                {
                    var attribute = method.GetCustomAttribute<T>();
                    if (attribute is not null)
                        methodsWithCustomAttribute.Add(method);
                }
            return methodsWithCustomAttribute;
        }
    }
}