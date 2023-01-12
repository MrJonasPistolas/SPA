namespace EXM.Common.Tools.Extensions
{
    public static class ByteArrayExtensions
    {
        public static Stream ToStream(this byte[] bytes)
        {
            return new MemoryStream(bytes)
            {
                Position = 0
            };
        }
    }
}
