using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Hadoop.Avro;

namespace SerializerTests.Serializers
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SerializerType("Microsoft.Hadoop.Avro", SerializerTypes.Binary)]

    public sealed class AvroSerializer<T> : TestBase<T, IAvroSerializer<T>> where T : class
    {
        public AvroSerializer(Func<int, T> testData, Action<T> dataToucher) : base(testData, dataToucher)
        {
            FormatterFactory = AvroSerializer.Create<T>;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected override void Serialize(T obj, Stream stream)
        {
            Formatter.Serialize(stream, obj);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected override T Deserialize(Stream stream)
        {
            return Formatter.Deserialize(stream);
        }
    }
}
