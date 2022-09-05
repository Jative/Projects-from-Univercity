namespace Lab4_14
{
    class Point
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public Point(string name = "O", float x = 0, float y = 0)
        {
            Name = name;
            X = x;
            Y = y;
        }
    }
}
