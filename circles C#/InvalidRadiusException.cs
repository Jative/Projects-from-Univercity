namespace Lab4_14
{
    internal class InvalidRadiusException : Exception
    {
        public InvalidRadiusException(float radius)
            : base($"Радиус {radius} невозможен!")
        { }
    }
}
