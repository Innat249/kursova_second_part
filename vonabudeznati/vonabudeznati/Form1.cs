namespace vonabudeznati
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.Text = "Паралельна аксонометрична проекція усіченої піраміди";
            this.Size = new Size(400, 400);
            this.Paint += new PaintEventHandler(DrawProjection);
        }

        private float angleX = (float)(Math.PI / 4); // Кут по X для аксонометрії
        private float angleY = (float)(Math.PI / 6); // Кут по Y для аксонометрії

        // Вершини усіченої піраміди (в 3D просторі)
        private PointF[] pyramidVertices = {
        new PointF(-50, 50),  // Нижня основа
        new PointF(50, 50),
        new PointF(50, -50),
        new PointF(-50, -50),
        new PointF(-25, 25),  // Верхня основа
        new PointF(25, 25),
        new PointF(25, -25),
        new PointF(-25, -25)
    };



        private PointF Project3DTo2D(PointF point3D)
        {
            // Перетворення 3D координат в 2D для аксонометричної проекції
            float x2D = point3D.X * (float)Math.Cos(angleX) - point3D.Y * (float)Math.Sin(angleX);
            float y2D = point3D.X * (float)Math.Sin(angleY) + point3D.Y * (float)Math.Cos(angleY);
            return new PointF(x2D, y2D);
        }

        private void DrawProjection(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(this.Width / 2, this.Height / 2); // Центрування проекції

            // Отримання 2D координат з 3D точок
            PointF[] projectedVertices = new PointF[pyramidVertices.Length];
            for (int i = 0; i < pyramidVertices.Length; i++)
            {
                projectedVertices[i] = Project3DTo2D(pyramidVertices[i]);
            }

            // Малювання основи (усіченої піраміди)
            g.DrawPolygon(Pens.Black, new PointF[] { projectedVertices[0], projectedVertices[1], projectedVertices[2], projectedVertices[3] });
            g.DrawPolygon(Pens.Black, new PointF[] { projectedVertices[4], projectedVertices[5], projectedVertices[6], projectedVertices[7] });

            // Малювання ребер між основами
            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(Pens.Black, projectedVertices[i], projectedVertices[i + 4]);
            }
        }
    }
}
