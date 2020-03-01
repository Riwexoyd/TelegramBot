using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetCoreTelegramBot.Helpers
{
    /// <summary>
    /// Хелпер рефлексии
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// Получение коллекции объектов определенного типа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICollection<T> GetInstance<T>()
        {
            var type = typeof(T);
            var types = GetImplimentationTypes(type).Where(i => !i.IsAbstract && !i.IsInterface);
            return types.Select(i => (T)Activator.CreateInstance(i)).ToList();
        }

        /// <summary>
        /// Получить список классов-наследников, не являющихся абстрактными классами или интерфейсами
        /// </summary>
        /// <param name="type">Тип, наследников которого необходимо получить</param>
        /// <returns>Список наследников типа</returns>
        public static ICollection<Type> GetImplimentationTypes(Type type)
        {
            return Assembly.GetAssembly(type)
                .GetTypes()
                .Where(i => type.IsAssignableFrom(i) && i != type && !i.IsAbstract && !i.IsInterface)
                .ToList();
        }
    }
}