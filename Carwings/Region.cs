namespace Carwings
{
    public class Region
    {
        public static readonly Region Usa = new Region("NNA");
        public static readonly Region Europe = new Region("NE");
        public static readonly Region Canada = new Region("NCI");
        public static readonly Region Australia = new Region("NMA");
        public static readonly Region Japan = new Region("NML");

        private readonly string value;

        private Region(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}