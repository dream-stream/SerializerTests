﻿using System.IO;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using NFX.Serialization.Slim;

namespace SerializerTests.Serializers
{
    /// <summary>
    /// https://github.com/aumcode/nfx/tree/master/Source/NFX/Serialization/Slim
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class SlimSerializer<T> : TestBase<T, SlimSerializer> where T:class
    {
       
        public SlimSerializer(Func<int, T> testData, Action<T> dataToucher) : base(testData, dataToucher)
        { 
            FormatterFactory = () =>
            {
              var types = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .Where(t => t.IsClass && t.Namespace == "SerializerTests.TypesToSerialize")
                                  .SelectMany(t => makeVariations(t));

              var result = new SlimSerializer( types );
              //Enable Batch mode for streaming messages, in that case the deserializing serializer has to be a different instance
              //result.TypeMode = TypeRegistryMode.Batch;
              return result;
            };
        }

#pragma warning disable IDE1006 // Naming Styles
        private IEnumerable<Type> makeVariations(Type t)
#pragma warning restore IDE1006 // Naming Styles
        {
          yield return t;
          yield return t.MakeArrayType();
          yield return typeof(List<>).MakeGenericType(t);
        }

        protected override T Deserialize(Stream stream)
        {
            return (T) Formatter.Deserialize(stream);
        }

        protected override void Serialize(T obj, Stream stream)
        {
            Formatter.Serialize(stream, obj);
        }
    }
}