﻿using System;
using System.IO;
using System.Runtime.Serialization;

namespace SerializerTests.Serializers
{
    public class DataContract<T> : TestBase<T, DataContractSerializer> where T : class
    {
        public DataContract(Func<int, T> testData, Action<T> dataToucher) : base(testData, dataToucher)
        {
            FormatterFactory = () => new DataContractSerializer(typeof(T));
        }

        protected override void Serialize(T obj, Stream stream)
        {
            Formatter.WriteObject(stream, obj);
        }

        protected override T Deserialize(Stream stream)
        {
            return (T)Formatter.ReadObject(stream);
        }
    }
}